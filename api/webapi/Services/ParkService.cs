using PPM.Interfaces;
using PPM.Models.DTOs;

namespace PPM.Services
{
    public class ParkService
    {
        private readonly IParkRepository _parkRepository;
        public ParkService(IParkRepository parkRepository)
        {
            _parkRepository = parkRepository;
        }

        public async Task<IEnumerable<ParkDTO>> GetAllParksInfoAsync()
        {
            var parks = await _parkRepository.GetAllParksAsync();
            var parksDTOs = parks.Select(park => new ParkDTO
            {
                park_id = park.park_id,
                park_name = park.park_name,
                geolocation = park.geolocation ?? string.Empty,
                street_address = park.street_address,
                city = park.city,
                state = park.state,
                zipcode = park.zipcode,
                number_of_pavillions = park.number_of_pavillions,
                acres = park.acres,
                play_structures = park.play_structures,
                trails = park.trails,
                baseball_fields = park.baseball_fields,
                disc_golf_courses = park.disc_golf_courses,
                volleyball_courts = park.volleyball_courts,
                fishing_spots = park.fishing_spots,
                soccer_fields = park.soccer_fields,
            });
            return parksDTOs;
        }
    }
}
