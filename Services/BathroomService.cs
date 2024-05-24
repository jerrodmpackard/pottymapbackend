using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pottymapbackend.Models;
using pottymapbackend.Services.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using pottymapbackend.Models.DTO;

namespace pottymapbackend.Services
{
    public class BathroomService
    {
        private readonly DataContext _context;

        public BathroomService(DataContext context)
        {
            _context = context;
        }
        public bool AddBathroom(BathroomModel newBathroom)
        {
            _context.Add(newBathroom);
            // Any return other than zero, we save changes. This is because this function is set up as a bool
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<BathroomModel> GetAllBathrooms()
        {
            return _context.BathroomInfo;
        }

        public string GetAllBathroomsAsGeoJSON()
        {
            // Your SQL query to generate GeoJSON data
            string sqlQuery = @"
                DECLARE @featureList nvarchar(max) =
                (
                    SELECT
                        'Feature'                                           as 'type',
                        id                                                  as 'properties.id',
                        name                                                as 'properties.name',
                        address                                             as 'properties.address',
                        city                                                as 'properties.city',
                        state                                               as 'properties.state',
                        zipCode                                             as 'properties.zipCode',
                        gender                                              as 'properties.gender',
                        type                                                as 'properties.type',
                        numberOfStalls                                      as 'properties.numberOfStalls',
                        wheelchairAccessibility                             as 'properties.wheelchairAccessibility',
                        hoursOfOperation                                    as 'properties.hoursOfOperation',
                        openToPublic                                        as 'properties.openToPublic',
                        keyRequired                                         as 'properties.keyRequired',
                        babyChangingStation                                 as 'properties.babyChangingStation',
                        cleanliness                                         as 'properties.cleanliness',
                        safety                                              as 'properties.safety',
                        'Point'                                             as 'geometry.type',
                        JSON_QUERY(CONCAT('[', CAST(longitude AS decimal(18, 15)), ', ', CAST(latitude AS decimal(18, 15)), ']')) as 'geometry.coordinates'
                    FROM BathroomInfo
                    FOR JSON PATH
                )

                DECLARE @featureCollection nvarchar(max) = (
                    SELECT 'FeatureCollection' as 'type',
                    JSON_QUERY(@featureList)   as 'features'
                    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
                )

                SELECT @featureCollection";

            using (SqlConnection connection = new SqlConnection(_context.Database.GetConnectionString()))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                connection.Open();

                string geoJSON = (string)command.ExecuteScalar();

                return geoJSON;
            }
        }

        public BathroomModel GetBathroomByID(int id)
        {
            return _context.BathroomInfo.SingleOrDefault(bathroom => bathroom.ID == id);
        }

        public bool UpdateBathroom(BathroomModel bathroomUpdate)
        {
            _context.Update<BathroomModel>(bathroomUpdate);
            return _context.SaveChanges() != 0;
        }

        public bool DeleteBathroom(BathroomModel bathroomToDelete)
        {
            bathroomToDelete.IsDeleted = true;
            _context.Update<BathroomModel>(bathroomToDelete);
            return _context.SaveChanges() != 0;
        }
    }
}