using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi_Server.Models;

namespace WebApi_Server.Controllers
{
    public class PowerloadsController : ApiController
    {
        [HttpPost]
        public string Post([FromBody] dynamic value)
        {
            string validString = Convert.ToString(value);
            Payload payload = JsonSerializer.Deserialize<Payload>(validString);

            Fuels fuels = new Fuels();
            fuels  = payload.fuels;

            List<Powerplants> powerpalants = new List<Powerplants>();
            powerpalants = payload.powerplants;
        
            ///////////////////RESPONSE////////////////////////
            ///
              ///////////////////LOAD/////////////////////////////
            double AmountEnergy = payload.Load;
            ///////////////////GAS///////////////////////////////////////////////////////////////////////////////////
            double PriceOfGasPerMWh = fuels.gas;
            ///////////////////KEROSINE/////////////////////////////////////////////
            double kerosine_euro_Mwh = payload.fuels.kerosine;
            ///////////////////CO2/////////////////////////////////////////////
            double co2_euro_ton = payload.fuels.co2;
            ///////////////////WIN/////////////////////////////////////////////////
            double windPercent = payload.fuels.wind;

            Plant_Power plant_Power = new Plant_Power();
            List<Plant_Power> plant_Powers = new List<Plant_Power>();           
            powerpalants.ForEach(b =>
            {
                plant_Power = new Plant_Power();
                plant_Power.name = b.name;

                
                ////////efficiency///////////
                double efficiency = b.efficiency;
                double pmax = b.pmax;
                //////////1  MWH Price/////////
                //////////////GAS///////////////////////////
                if (b.type == "gasfired")
                {
                    //double test = 6 / 0.50;
                    double One_MWH = PriceOfGasPerMWh / efficiency;
                    plant_Power.p = One_MWH;
                }
                //////////////KEROSINE///////////////////////////
                if (b.type == "turbojet")
                {
                    double One_MWH = kerosine_euro_Mwh;
                    plant_Power.p = One_MWH;
                }
                //////////////WIND///////////////////////////
                if (b.type == "windturbine")
                {
                    //double test = 4 * 0.25;
                    double One_MWH = pmax * efficiency;
                    plant_Power.p = One_MWH;
                }
                ////////////////////////////////////////////

                //////////////////CALCULATIONS////////////////////

                plant_Powers.Add(plant_Power);
            });
            string Strplant_Powers = JsonSerializer.Serialize(plant_Powers);

            return Strplant_Powers;// "Hello from http post web api controller: ";           

        }
    }
}
