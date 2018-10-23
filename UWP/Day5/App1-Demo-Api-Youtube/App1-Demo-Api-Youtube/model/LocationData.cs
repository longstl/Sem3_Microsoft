using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace App1_Demo_Api_Youtube.model
{
    public class LocationData
    {
        public async static Task<Geoposition> getPosition()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus != GeolocationAccessStatus.Allowed) throw new Exception();
            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            var postion = await geolocator.GetGeopositionAsync();
            return postion;
        }
    }
}