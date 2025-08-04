using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PPM.Interfaces;
using PPM.Models.DTOs;
using PPM.Models.Interfaces;
using PPM.Repositories;
using PPM.Services;

namespace PPM.Models.Services
{
    public class RegistrationService
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IParkRepository _parkRepository;
        private readonly UserService _userService;
        private readonly EventService _eventService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RegistrationService> _logger;

        public RegistrationService(IRegistrationRepository registrationRepository, IUserRepository userRepository, IParkRepository parkRepository, UserService userService, EventService eventService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogger<RegistrationService> logger)
        {
            _registrationRepository = registrationRepository;
            _userRepository = userRepository;
            _parkRepository = parkRepository;
            _userService = userService;
            _eventService = eventService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public async Task<RegistrationDTO> CreateRegistration(RegistrationDTO registrationDTO)
        {
            var user_id = _userService.GetLoggedInUserId();
            if (user_id == null)
            {
                throw new ApplicationException("User is not Logged In.");
            }
            var user = await _userRepository.GetUserByIdAsync(user_id.Value);
            if (user == null)
            {
                throw new ApplicationException("User does not exist.");
            }
            var park = await _parkRepository.GetParkByIdAsync(registrationDTO.park_id);
            if (park == null)
            {
                throw new ApplicationException("Park is currently Null.");
            }
            var registration = new Registration
            {
                user_id = user.user_id,
                park_id = park.park_id,
                requested_park = registrationDTO.requested_park,
                pavillion = registrationDTO.pavillion,
                registration_date = registrationDTO.registration_date,
                start_time = registrationDTO.start_time,
                end_time = registrationDTO.end_time,
                is_approved = false,
                is_reviewed = false,
                User = user, //Navigation Properties
                Park = park  //Navigation Properties
            };
            var createdRegistration = await _registrationRepository.CreateRegistrationAsync(registration);
            return new RegistrationDTO
            {
                registration_id = createdRegistration.registration_id,
                user_id = createdRegistration.user_id,
                park_id = createdRegistration.park_id,
                requested_park = createdRegistration.requested_park,
                pavillion = createdRegistration.pavillion,
                registration_date = createdRegistration.registration_date,
                start_time = createdRegistration.start_time,
                end_time = createdRegistration.end_time,
                is_approved = createdRegistration.is_approved,
                is_reviewed=createdRegistration.is_reviewed,
                User = new UserDTO
                {
                    first_name = user.first_name,
                    last_name = user.last_name,

                },
                Park = new ParkDTO
                {
                    park_name = park.park_name,
                    street_address = park.street_address,
                    city = park.city,
                    state = park.state,
                    zipcode = park.zipcode,
                }
            };

        }
        public async Task<RegistrationDTO> GetRegistrationDetailsAsync(int registration_id)
        {
            var user_id = _userService.GetLoggedInUserId();
            if (user_id == null)
            {
                throw new ApplicationException("User is not Logged In.");
            }
            var user = await _userRepository.GetUserByIdAsync(user_id.Value);
            if (user == null)
            {
                throw new ApplicationException("User does not exist.");
            }
            var registration = await _registrationRepository.GetRegistrationByIdAsync(registration_id);
            if (registration == null || registration_id != user_id.Value)
            {
                throw new ApplicationException("Registration does not exist.");
            }
            var park = await _parkRepository.GetParkByIdAsync(registration.park_id);
            if (park == null)
            {
                throw new ApplicationException("Park is currently Null.");
            }
            return new RegistrationDTO
            {
                registration_id = registration.registration_id,
                user_id = registration.user_id,
                park_id = registration.park_id,
                requested_park = registration.requested_park,
                pavillion = registration.pavillion,
                registration_date = registration.registration_date,
                start_time = registration.start_time,
                end_time = registration.end_time,
                is_approved = registration.is_approved,
                is_reviewed = registration.is_reviewed,
                User = new UserDTO
                {
                    first_name = user.first_name,
                    last_name = user.last_name,
                },
                Park = new ParkDTO
                {
                    park_name = park.park_name,
                    street_address = park.street_address,
                    city = park.city,
                    state = park.state,
                    zipcode = park.zipcode,
                }
            };
        }
        public async Task<IEnumerable<RegistrationDTO>> GetAllUserRegistrationsAsync()
        {
            var logged_in_user_id = _userService.GetLoggedInUserId();
            if (logged_in_user_id == null)
            {
                throw new ApplicationException("User is not Logged In.");
            }
            var user = await _userRepository.GetUserByIdAsync(logged_in_user_id.Value);
            if (user == null)
            {
                throw new ApplicationException("User does not exist or is not Authorized.");
            }
            var registration_inquiries = await _registrationRepository.GetAllUserRegistrationsAsync(logged_in_user_id.Value);
            if(registration_inquiries == null)
            {
                return Enumerable.Empty<RegistrationDTO>();
            }
            var registrationInquiriesDTOs = registration_inquiries.Select(registration_inquiry => new RegistrationDTO
            {
                registration_id = registration_inquiry.registration_id,
                user_id = registration_inquiry.user_id,
                park_id = registration_inquiry.park_id,
                requested_park = registration_inquiry.requested_park,
                pavillion = registration_inquiry.pavillion,
                registration_date = registration_inquiry.registration_date,
                start_time = registration_inquiry.start_time,
                end_time = registration_inquiry.end_time,
                is_approved = registration_inquiry.is_approved,
                is_reviewed = registration_inquiry.is_reviewed,
                User = new UserDTO
                {
                    first_name = registration_inquiry.User?.first_name,
                    last_name = registration_inquiry.User?.last_name,
                },
                Park = new ParkDTO
                {
                    park_name = registration_inquiry.Park.park_name,
                    street_address = registration_inquiry.Park.street_address,
                    city = registration_inquiry.Park.city,
                    state = registration_inquiry.Park.state,
                    zipcode = registration_inquiry.Park.zipcode,
                }
            });
            return registrationInquiriesDTOs;
        }
        //Retrieves All Registration Inquiries from all Users in the Database for the Admin to View
        public async Task<IEnumerable<RegistrationDTO>> GetAllAdminRegistrationsAsync()
        {
            var logged_in_user_id = _userService.GetLoggedInUserId();
            if (logged_in_user_id == null)
            {
                throw new ApplicationException("User is not Logged In.");
            }
            var user = await _userRepository.GetUserByIdAsync(logged_in_user_id.Value);
            if (user == null && !user.is_admin)
            {
                throw new ApplicationException("User does not exist or is not Authorized.");
            }
            var registration_inquiries = await _registrationRepository.GetAllAdminRegistrationsAsync();
            if (registration_inquiries == null)
            {
                return Enumerable.Empty<RegistrationDTO>();
            }
            var registrationInquiriesDTOs = registration_inquiries.Select(registration_inquiry => new RegistrationDTO
            {
                registration_id = registration_inquiry.registration_id,
                user_id = registration_inquiry.user_id,
                park_id = registration_inquiry.park_id,
                requested_park = registration_inquiry.requested_park,
                pavillion = registration_inquiry.pavillion,
                registration_date = registration_inquiry.registration_date,
                start_time = registration_inquiry.start_time,
                end_time = registration_inquiry.end_time,
                is_approved = registration_inquiry.is_approved,
                is_reviewed = registration_inquiry.is_reviewed,
                User = new UserDTO
                {
                    first_name = registration_inquiry.User?.first_name,
                    last_name = registration_inquiry.User?.last_name,
                },
                Park = new ParkDTO
                {
                    park_name = registration_inquiry.Park.park_name,
                    street_address = registration_inquiry.Park.street_address,
                    city = registration_inquiry.Park.city,
                    state = registration_inquiry.Park.state,
                    zipcode = registration_inquiry.Park.zipcode,
                }
            });
            return registrationInquiriesDTOs;
        }
        public async Task<IEnumerable<RegistrationDTO>> GetAllUnreviewedRegistrationsAsync()
        {
            var logged_in_user_id = _userService.GetLoggedInUserId();
            if (logged_in_user_id == null)
            {
                throw new ApplicationException("User is not Logged In.");
            }
            var user = await _userRepository.GetUserByIdAsync(logged_in_user_id.Value);
            if (user == null && !user.is_admin)
            {
                throw new ApplicationException("User does not exist or is not Authorized.");
            }
            var unreviewed_registration_inquiries = await _registrationRepository.GetAllUnreviewedRegistrationsAsync();
            if (unreviewed_registration_inquiries == null)
            {
                return Enumerable.Empty<RegistrationDTO>();
            }
            var registrationInquiriesDTOs = unreviewed_registration_inquiries.Select(registration_inquiry => new RegistrationDTO{
                registration_id = registration_inquiry.registration_id,
                user_id = registration_inquiry.user_id,
                park_id = registration_inquiry.park_id,
                requested_park = registration_inquiry.requested_park,
                pavillion = registration_inquiry.pavillion,
                registration_date = registration_inquiry.registration_date,
                start_time = registration_inquiry.start_time,
                end_time = registration_inquiry.end_time,
                is_approved = registration_inquiry.is_approved,
                is_reviewed = registration_inquiry.is_reviewed,
                User = new UserDTO
                {
                    first_name = registration_inquiry.User?.first_name,
                    last_name = registration_inquiry.User?.last_name,
                },
                Park = new ParkDTO
                {
                    park_name = registration_inquiry.Park.park_name,
                    street_address = registration_inquiry.Park.street_address,
                    city = registration_inquiry.Park.city,
                    state = registration_inquiry.Park.state,
                    zipcode = registration_inquiry.Park.zipcode,
                }
            });
            return registrationInquiriesDTOs;
        }
        public async Task<RegistrationDTO> RejectRegistration(int registration_id)
        {
            var logged_in_user_id = _userService.GetLoggedInUserId();
            if (logged_in_user_id == null)
            {
                throw new ApplicationException("User is not Logged In.");
            }
            var logged_in_user = await _userRepository.GetUserByIdAsync(logged_in_user_id.Value);
            if (logged_in_user == null || !logged_in_user.is_admin)
            {
                throw new UnauthorizedAccessException("User does not exist.");
            }

            var registration = await _registrationRepository.GetRegistrationByIdAsync(registration_id);
            if (registration == null)
            {
                throw new ApplicationException("Registration does not exist.");
            }
            registration.is_approved = false;
            registration.is_reviewed = true;
            await _registrationRepository.UpdateRegistrationAsync(registration);

            var user = await _userRepository.GetUserByIdAsync(registration.user_id);
            if (user == null)
            {
                throw new ApplicationException("User is currently Null.");
            }
            var park = await _parkRepository.GetParkByIdAsync(registration.park_id);
            if (park == null)
            {
                throw new ApplicationException("Park is currently Null.");
            }

            return new RegistrationDTO
            {
                registration_id = registration.registration_id,
                user_id = registration.user_id,
                park_id = registration.park_id,
                requested_park = registration.requested_park,
                pavillion = registration.pavillion,
                registration_date = registration.registration_date,
                start_time = registration.start_time,
                end_time = registration.end_time,
                is_approved = registration.is_approved,
                is_reviewed = registration.is_reviewed,
                User = new UserDTO
                {
                    first_name = user.first_name,
                    last_name = user.last_name,
                },
                Park = new ParkDTO
                {
                    park_name = park.park_name,
                    street_address = park.street_address,
                    city = park.city,
                    state = park.state,
                    zipcode = park.zipcode,
                }
            };
        }
        public async Task<RegistrationDTO> ApproveRegistration(int registration_id)
        {
            var logged_in_user_id = _userService.GetLoggedInUserId();
            if (logged_in_user_id == null)
            {
                throw new ApplicationException("User is not Logged In.");
            }
            var logged_in_user = await _userRepository.GetUserByIdAsync(logged_in_user_id.Value);
            if (logged_in_user == null || !logged_in_user.is_admin)
            {
                throw new UnauthorizedAccessException("User does not exist.");
            }

            var registration = await _registrationRepository.GetRegistrationByIdAsync(registration_id);
            if (registration == null)
            {
                throw new ApplicationException("Registration does not exist.");
            }
            registration.is_approved = true;
            registration.is_reviewed = true;
            await _registrationRepository.UpdateRegistrationAsync(registration);


            var user = await _userRepository.GetUserByIdAsync(registration.user_id);
            if (user == null)
            {
                throw new ApplicationException("User is currently Null.");
            }
            var park = await _parkRepository.GetParkByIdAsync(registration.park_id);
            if (park == null)
            {
                throw new ApplicationException("Park is currently Null.");
            }
            var approvedRegistrationDTOs = new RegistrationDTO
            {
                registration_id = registration.registration_id,
                user_id = registration.user_id,
                park_id = registration.park_id,
                requested_park = registration.requested_park,
                pavillion = registration.pavillion,
                registration_date = registration.registration_date,
                start_time = registration.start_time,
                end_time = registration.end_time,
                is_approved = registration.is_approved,
                is_reviewed = registration.is_reviewed,
                User = new UserDTO
                {
                    first_name = user.first_name,
                    last_name = user.last_name,
                },
                Park = new ParkDTO
                {
                    park_name = park.park_name,
                    street_address = park.street_address,
                    city = park.city,
                    state = park.state,
                    zipcode = park.zipcode,
                }
            };
            var ev = new EventDTO
            {
                user_id = approvedRegistrationDTOs.user_id,
                park_id = approvedRegistrationDTOs.park_id,
                registration_id = approvedRegistrationDTOs.registration_id,
                event_name = "Event for " + approvedRegistrationDTOs.User.first_name + " " + approvedRegistrationDTOs.User.last_name + " at " + approvedRegistrationDTOs.requested_park,
                event_desc = "Auto-created event for approved registration",
                event_start_date = approvedRegistrationDTOs.registration_date,
                event_end_date = approvedRegistrationDTOs.registration_date,
                event_start_time = approvedRegistrationDTOs.start_time,
                event_end_time = approvedRegistrationDTOs.end_time,
                User = approvedRegistrationDTOs.User,
                Park = approvedRegistrationDTOs.Park,
            };
            await _eventService.CreateEventAsync(ev);
            return approvedRegistrationDTOs;
        }
    }
}
