using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Shared.Dto.Address;
using RefugeUA.WebApp.Server.Shared.Dto.ContactInformation;

namespace RefugeUA.WebApp.Server.Extensions.Mapping
{
    public static class DtoToEntityMapping
    {
        public static Address MapToEntity(this AddressDto addressDto)
        {
            return new Address()
            {
                Country = addressDto.Country,
                Region = addressDto.Region,
                District = addressDto.District,
                Settlement = addressDto.Settlement,
                Street = addressDto.Street,
                PostalCode = addressDto.PostalCode,
            };
        }

        public static void MapToExistingEntityFull(this AddressDto addressDto, Address address)
        {
            address.Country = addressDto.Country;
            address.Region = addressDto.Region;
            address.District = addressDto.District;
            address.Settlement = addressDto.Settlement;
            address.Street = addressDto.Street;
            address.PostalCode = addressDto.PostalCode;
        }

        public static ContactInformation MapToEntity(this ContactInformationDto contactInformationDto)
        {
            return new ContactInformation()
            {
                PhoneNumber = contactInformationDto.PhoneNumber,
                Email = contactInformationDto.Email,
                Telegram = contactInformationDto.Telegram,
                Viber = contactInformationDto.Viber,
                Facebook = contactInformationDto.Facebook,
            };
        }

        public static void MapToExistingEntityFull(this ContactInformationDto contactInfoDto, ContactInformation contactInfo)
        {
            contactInfo.PhoneNumber = contactInfoDto.PhoneNumber;
            contactInfo.Email = contactInfoDto.Email;
            contactInfo.Telegram = contactInfoDto.Telegram;
            contactInfo.Viber = contactInfoDto.Viber;
            contactInfo.Facebook = contactInfoDto.Facebook;
        }
    }
}
