using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Nancy.Json;

namespace Homemade.BLL.General
{
    public class GoogleGeoCodeResp
    {
        public class GoogleGeoCodeResponse
        {
            public string status { get; set; }
            public results[] results { get; set; }
        }

        public class results
        {
            public string formatted_address { get; set; }
            public geometry geometry { get; set; }
            public string[] types { get; set; }
            public address_component[] address_components { get; set; }
        }

        public class geometry
        {
            public string location_type { get; set; }
            public location location { get; set; }
        }

        public class location
        {
            public string lat { get; set; }
            public string lng { get; set; }
        }

        public class address_component
        {
            public string long_name { get; set; }
            public string short_name { get; set; }
            public string[] types { get; set; }
        }

        public static Coordinate GetCoordinates(string lat, string lang, string googlekey)
        {
            using (var client = new WebClient())
            {
                string uri = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + lang + "&key=" + googlekey + "";

                string geocodeInfo = client.DownloadString(uri);
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                GoogleGeoCodeResponse latlongdata = oJS.Deserialize<GoogleGeoCodeResponse>(geocodeInfo);


                return new Coordinate(Convert.ToDouble(latlongdata.results[0].geometry.location.lat), Convert.ToDouble(latlongdata.results[0].geometry.location.lng));
            }
        }
        public bool CheckIsCorrectLatLong(string lat, string lang, string googlekey)
        {
            using (var client = new WebClient())
            {
                string uri = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + lang + "&key=" + googlekey + "";

                try
                {
                    string geocodeInfo = client.DownloadString(uri);
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    GoogleGeoCodeResponse latlongdata = oJS.Deserialize<GoogleGeoCodeResponse>(geocodeInfo);

                    if (latlongdata.status == "OK")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public struct Coordinate
        {
            private double lat;
            private double lng;

            public Coordinate(double latitude, double longitude)
            {
                lat = latitude;
                lng = longitude;

            }

            public double Latitude { get { return lat; } set { lat = value; } }
            public double Longitude { get { return lng; } set { lng = value; } }

        }
        public class ShareLocation
        {
            public string areaName { get; set; }
            public string countryName { get; set; }
            public string CityName { get; set; }
            public string Citylat { get; set; }
            public string Citylong { get; set; }
            public string LocationName { get; set; }
            public string Blok_place_id { get; set; }
            public string Blok_Lat { get; set; }
            public string Blok_Long { get; set; }
            public string Blok_LocationType { get; set; }
        }
        public class BlokArray
        {
            public string Blok_Lat { get; set; }
            public string Blok_Long { get; set; }
            public string Blok_place_id { get; set; }
            public string LocationType { get; set; }
            public string BlockName { get; set; }
        }
        public ShareLocation GetReciverData(string lat, string lang, string googlekey)
        {
            try
            {
                ShareLocation ShareLocation = new ShareLocation();
                using (var client = new WebClient())
                {
                    string uri = "https://maps.googleapis.com/maps/api/geocode/json?latlng=" + lat + "," + lang + "&key=" + googlekey + "&language=ar&region=SA";
                    var LocationName = "";
                    var neighborhood = "";
                    var neighborhood_Lat = "";
                    var neighborhood_Long = "";
                    var neighborhoodLocationType = "";
                    var neighborhood_place_id = "";
                    var political = "";
                    var political_Lat = "";
                    var political_Long = "";
                    var political_bounds_northeast_Lat = "";
                    var political_place_id = "";
                    var sublocality = "";
                    var sublocality_Lat = "";
                    var sublocality_Long = "";
                    var sublocality_viewport_southwest_Long = "";
                    var sublocality_place_id = "";
                    var CityName = "";
                    var City_Lat = "";
                    var City_Long = "";
                    var sublocality_level_1 = "";
                    var sublocality_level_1_Lat = "";
                    var sublocality_level_1_Long = "";
                    var sublocality_level_1_viewport_southwest_Long = "";
                    var sublocality_level_1_place_id = "";
                    var locality = "";
                    var locality_Lat = "";
                    var locality_Long = "";
                    var locality_viewport_southwest_Long = "";
                    var locality_place_id = "";
                    var administrative_area_level_1 = "";
                    var administrative_area_level_1_viewport_southwest_Long = "";
                    var administrative_area_level_1_Lat = "";
                    var administrative_area_level_1_Long = "";
                    var administrative_area_level_1_place_id = "";
                    var administrative_area_level_2 = "";
                    var administrative_area_level_2_Lat = "";
                    var administrative_area_level_2_Long = "";
                    var administrative_area_level_2_viewport_southwest_Long = "";
                    var administrative_area_level_2_place_id = "";
                    var countryName = "";
                    var country_Lat = "";
                    var country_viewport_southwest_Long = "";
                    var country_Long = "";
                    var country_place_id = "";
                    var City_viewport_southwest_Long = "";
                    var City_place_id = "";
                    var areaName = "";
                    var areaType = "";
                    HttpClient httpClient = new HttpClient();
                    string result = httpClient.GetStringAsync(uri).Result;
                    // Get all json data
                    GoogleGeoCodeResponse jsonData = new JavaScriptSerializer().Deserialize<GoogleGeoCodeResponse>(result);
                    // Extract just the first array element (This is the only data we are interested in)
                    var xx = jsonData.results;
                    List<BlokArray> BlokArrayList = new List<BlokArray>();

                    for (var i = 0; i < xx.Length; i++)
                    {
                        var types = xx[i].types;
                        var type = types[0];
                        var nn = xx[i].address_components;
                        var conn = nn[0];

                        if (type == "neighborhood")
                        {
                            LocationName = conn.long_name + " , " + LocationName;
                            neighborhood = conn.long_name;
                            neighborhood_Lat = xx[i].geometry.location.lat;
                            neighborhood_Long = xx[i].geometry.location.lng;
                            neighborhoodLocationType = xx[i].geometry.location_type;
                            neighborhood_place_id = xx[i].formatted_address;
                        }
                        else if (type == "political")
                        {
                            political = conn.long_name;
                            political_Lat = xx[i].geometry.location.lat;
                            political_Long = xx[i].geometry.location.lng;
                            political_bounds_northeast_Lat = xx[i].geometry.location_type;
                            political_place_id = xx[i].formatted_address;
                        }
                        else if (type == "sublocality")
                        {
                            sublocality = conn.long_name;
                            sublocality_Lat = xx[i].geometry.location.lat;
                            sublocality_Long = xx[i].geometry.location.lng;
                            sublocality_viewport_southwest_Long = xx[i].geometry.location_type;
                            sublocality_place_id = xx[i].formatted_address;
                        }
                        else if (type == "sublocality_level_1")
                        {
                            sublocality_level_1 = conn.long_name;
                            sublocality_level_1_Lat = xx[i].geometry.location.lat;
                            sublocality_level_1_Long = xx[i].geometry.location.lng;
                            sublocality_level_1_viewport_southwest_Long = xx[i].geometry.location_type;
                            sublocality_level_1_place_id = xx[i].formatted_address;
                        }
                        else if (type == "locality")
                        {
                            locality = conn.long_name;
                            locality_Lat = xx[i].geometry.location.lat;
                            locality_Long = xx[i].geometry.location.lng;
                            locality_viewport_southwest_Long = xx[i].geometry.location_type;
                            locality_place_id = xx[i].formatted_address;
                        }
                        else if (type == "administrative_area_level_1")
                        {
                            administrative_area_level_1 = conn.long_name;
                            administrative_area_level_1_Lat = xx[i].geometry.location.lat;
                            administrative_area_level_1_Long = xx[i].geometry.location.lng;
                            administrative_area_level_1_viewport_southwest_Long = xx[i].geometry.location_type;
                            administrative_area_level_1_place_id = xx[i].formatted_address;
                        }
                        else if (type == "administrative_area_level_2")
                        {
                            administrative_area_level_2 = conn.long_name;
                            administrative_area_level_2_Lat = xx[i].geometry.location.lat;
                            administrative_area_level_2_Long = xx[i].geometry.location.lng;
                            administrative_area_level_2_viewport_southwest_Long = xx[i].geometry.location_type;
                            administrative_area_level_2_place_id = xx[i].formatted_address;
                        }
                        if (type == "country")
                        {
                            countryName = conn.long_name;
                            country_Lat = xx[i].geometry.location.lat;
                            country_Long = xx[i].geometry.location.lng;
                            country_viewport_southwest_Long = xx[i].geometry.location_type;
                            country_place_id = xx[i].formatted_address;
                        }
                        if (type == "route")
                        {
                            for (var xxi = 0; xxi < xx[i].address_components.Length; xxi++)
                            {
                                if (xx[i].address_components[xxi].types[0] == "route")
                                {
                                    if (!xx[i].address_components[xxi].long_name.Contains("Unnamed Road"))
                                    {
                                        if (xx[i].address_components[xxi].long_name.Contains("شارع"))
                                        {
                                            LocationName = xx[i].address_components[xxi].long_name;
                                        }
                                        else if (xx[i].address_components[xxi].long_name.Contains("طريق"))
                                        {
                                            LocationName = xx[i].address_components[xxi].long_name;
                                        }
                                        else
                                        {
                                            LocationName = "شارع " + xx[i].address_components[xxi].long_name;
                                        }
                                    }
                                }
                                if (xx[i].address_components[xxi].types.Contains("sublocality") || xx[i].address_components[xxi].types.Contains("sublocality_level_1"))
                                {
                                    if (!LocationName.Contains(xx[i].address_components[xxi].long_name) && !string.IsNullOrWhiteSpace(xx[i].address_components[xxi].long_name))
                                    {
                                        LocationName = " حى " + xx[i].address_components[xxi].long_name + ", " + LocationName;
                                    }
                                }
                                if (xx[i].address_components[xxi].types.Contains("administrative_area_level_2") || xx[i].address_components[xxi].types.Contains("locality"))
                                {
                                    if (!LocationName.Contains(xx[i].address_components[xxi].long_name) && !string.IsNullOrWhiteSpace(xx[i].address_components[xxi].long_name))
                                    {
                                        LocationName = LocationName + ", " + xx[i].address_components[xxi].long_name;
                                    }
                                }
                                if (xx[i].address_components[xxi].types.Contains("postal_code"))
                                {
                                    if (!LocationName.Contains(xx[i].address_components[xxi].long_name) && !string.IsNullOrWhiteSpace(xx[i].address_components[xxi].long_name))
                                    {
                                        LocationName = LocationName + ", " + xx[i].address_components[xxi].long_name;
                                    }
                                }
                                if (xx[i].address_components[xxi].types.Contains("country"))
                                {
                                    if (!LocationName.Contains(xx[i].address_components[xxi].long_name) && !string.IsNullOrWhiteSpace(xx[i].address_components[xxi].long_name))
                                    {
                                        LocationName = LocationName + ", " + xx[i].address_components[xxi].long_name;
                                    }
                                }

                            }

                        }

                        if (type == "locality")
                        {
                            CityName = conn.long_name;
                            City_Lat = xx[i].geometry.location.lat;
                            City_Long = xx[i].geometry.location.lng;
                            City_viewport_southwest_Long = xx[i].geometry.location_type;
                            City_place_id = xx[i].formatted_address;
                        }
                    }
                    if (neighborhood.Length > 0)
                    {
                        areaName = neighborhood;
                        areaType = "neighborhood";
                    }
                    else if (sublocality.Length > 0)
                    {
                        areaName = sublocality;
                        areaType = "sublocality";

                    }
                    else if (sublocality_level_1.Length > 0)
                    {
                        areaName = sublocality_level_1;
                        areaType = "sublocality_level_1";

                    }
                    else if (sublocality.Length > 0)
                    {
                        areaName = sublocality;
                        areaType = "sublocality";

                    }
                    else if (political.Length > 0)
                    {
                        areaName = political;
                        areaType = "political";

                    }
                    else if (locality.Length > 0)
                    {
                        areaName = locality;
                        //areaName = locality;
                        areaType = "locality";

                    }
                    else if (administrative_area_level_1.Length > 0)
                    {
                        areaName = administrative_area_level_1;
                        areaType = "administrative_area_level_1";

                    }
                    else if (administrative_area_level_2.Length > 0)
                    {
                        areaName = administrative_area_level_2;
                        areaType = "administrative_area_level_2";

                    }
                    if (string.IsNullOrWhiteSpace(CityName))
                    {
                        CityName = administrative_area_level_2;
                        City_Lat = administrative_area_level_2_Lat;
                        City_Long = administrative_area_level_2_Long;
                        City_place_id = administrative_area_level_2_place_id;
                    }
                    if (areaType == "neighborhood")
                    {
                        BlokArray BlokArray = new BlokArray();
                        BlokArray.Blok_place_id = neighborhood_place_id;
                        BlokArray.Blok_Lat = neighborhood_Lat;
                        BlokArray.Blok_Long = neighborhood_Long;
                        BlokArray.LocationType = neighborhoodLocationType;
                        BlokArray.BlockName = neighborhood;
                        if (BlokArray.Blok_place_id.Contains("Ø§Ø¶"))
                        {
                            BlokArray.Blok_place_id = political_place_id;
                            BlokArray.Blok_Lat = political_Lat;
                            BlokArray.Blok_Long = political_Long;
                            BlokArray.LocationType = political_bounds_northeast_Lat;
                            BlokArray.BlockName = political;
                            areaName = political;
                            areaType = "political";
                        }
                        BlokArrayList.Add(BlokArray);


                    }
                    else if (areaType == "sublocality_level_1")
                    {
                        BlokArray BlokArray = new BlokArray();
                        BlokArray.Blok_place_id = sublocality_level_1_place_id;
                        BlokArray.Blok_Lat = sublocality_level_1_Lat;
                        BlokArray.Blok_Long = sublocality_level_1_Long;
                        BlokArray.LocationType = sublocality_level_1_viewport_southwest_Long;
                        BlokArray.BlockName = sublocality_level_1;

                        BlokArrayList.Add(BlokArray);
                    }
                    else if (areaType == "sublocality")
                    {
                        BlokArray BlokArray = new BlokArray();
                        BlokArray.Blok_place_id = sublocality_place_id;
                        BlokArray.Blok_Lat = sublocality_Lat;
                        BlokArray.Blok_Long = sublocality_Long;
                        BlokArray.LocationType = sublocality_viewport_southwest_Long;
                        BlokArray.BlockName = sublocality;

                        BlokArrayList.Add(BlokArray);
                    }
                    else if (areaType == "political")
                    {
                        BlokArray BlokArray = new BlokArray();
                        BlokArray.Blok_place_id = political_place_id;
                        BlokArray.Blok_Lat = political_Lat;
                        BlokArray.Blok_Long = political_Long;
                        BlokArray.LocationType = political_bounds_northeast_Lat;
                        BlokArray.BlockName = political;

                        BlokArrayList.Add(BlokArray);
                    }
                    else if (areaType == "locality")
                    {
                        BlokArray BlokArray = new BlokArray();
                        BlokArray.Blok_place_id = locality_place_id;
                        BlokArray.Blok_Lat = locality_Lat;
                        BlokArray.Blok_Long = locality_Long;
                        BlokArray.LocationType = locality_viewport_southwest_Long;
                        BlokArray.BlockName = locality;

                        BlokArrayList.Add(BlokArray);
                    }
                    else if (areaType == "administrative_area_level_1")
                    {
                        BlokArray BlokArray = new BlokArray();
                        BlokArray.Blok_place_id = administrative_area_level_1_place_id;
                        BlokArray.Blok_Lat = administrative_area_level_1_Lat;
                        BlokArray.Blok_Long = administrative_area_level_1_Long;
                        BlokArray.LocationType = administrative_area_level_1_viewport_southwest_Long;
                        BlokArray.BlockName = administrative_area_level_1;

                        BlokArrayList.Add(BlokArray);
                    }
                    else if (areaType == "administrative_area_level_2")
                    {
                        BlokArray BlokArray = new BlokArray();
                        BlokArray.Blok_place_id = administrative_area_level_2_place_id;
                        BlokArray.Blok_Lat = administrative_area_level_2_Lat;
                        BlokArray.Blok_Long = administrative_area_level_2_Long;
                        BlokArray.LocationType = administrative_area_level_2_viewport_southwest_Long;
                        BlokArray.BlockName = administrative_area_level_2;

                        BlokArrayList.Add(BlokArray);
                    }
                    if ((areaName != "Avon" && !areaName.Contains("Ø§Ø¶") && countryName != "أستراليا" && countryName != "Australia") && (countryName == "السعودية" || countryName == "Saudi" || countryName == "Saudi Arabia"))
                    {
                        ShareLocation.areaName = areaName;
                        ShareLocation.Blok_Lat = BlokArrayList[0].Blok_Lat;
                        ShareLocation.Blok_Long = BlokArrayList[0].Blok_Long;
                        ShareLocation.Blok_place_id = LocationName;
                        ShareLocation.Blok_LocationType = BlokArrayList[0].BlockName;
                        ShareLocation.CityName = CityName;
                        ShareLocation.countryName = countryName;
                        ShareLocation.Citylat = City_Lat;
                        ShareLocation.Citylong = City_Long;
                    }
                    else
                    {
                        ShareLocation.Blok_place_id = LocationName;
                    }
                    return ShareLocation;


                }
            }
            catch (Exception)
            {
                return null;
                //throw;
            }

        }
    }
}
