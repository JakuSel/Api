using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IBANController : Controller
    {
        [HttpPost]
        public JsonResult Iban([FromBody] Ucet ucet)
        {
            return !(ucet.code.Length != 4 || ucet.acc.Length > 10) ? 
                Json(new { Iban = IbanString(ucet), Banka = BankaString(ucet) }) : 
                Json(null);
        }

        public int Modulo(string bban)
        {
            var k = 0;
            for(int i = 0; i < bban.Length;i++)
                k = (k*10+Int32.Parse((bban.ToString()[i]).ToString()) )% 97;
            
            return k;
        }

        public string IbanString(Ucet ucet)
        {
            var cisloUctu = (((ucet.acc)).PadLeft(10, '0')).ToString();
            var banka = ucet.code;
            var prefix = "000000";
            var SK = "282000";
            var checksum = banka + prefix + cisloUctu + SK;
            return string.Concat("SK", ((98 - Modulo(checksum)).ToString()), " ",
                " ",ucet.code," ","0000 00"," ",cisloUctu);
        }
        public string BankaString(Ucet ucet)
        {
            string[] kodBanky = { "5200","0200","0900","0720","1100","1111","3000","3100","5900",
            "6500","7300","7500","7930","8050","8100","8120","8130","8160","8170","8180","8300",
            "8320","8330","8360","8370","8410"};
            string[] nazovBanky = { "OTP Banka Slovensko, a.s.", "Všeobecná úverová banka, a.s.",
            "Slovenská sporiteľňa, a.s.","Národná banka Slovenska","Tatra Banka,a.s.",
                "UniCredit Bank Slovakia,a.s.","Slovenská záručná a rozvojová banka, a.s.",
            "VOLKSBANK Slovensko, a.s.","Prvá stavebná sporiteľňa, a.s.","Poštová banka, a.s.",
            "ING Bank N.V., pobočka zahraničnej banky","Československá obchodná banka, a.s.",
            "Wüstenrot stavebná sporiteľňa, a.s.","COMMERZBANK Aktiengesellschaft, pobočka zahraničnej banky, Bratislava,",
            "Komerční banka Bratislava, a.s.","Privatbanka, a.s.,","Citibank Europe plc, pobočka zahraničnej banky",
            "EXIMBANKA SR","ČSOB stavebná sporiteľňa, a. s.","Štátna pokladnica","HSBC Bank plc, pobočka zahraničnej banky",
            "J&T BANKA, a.s., pobočka zahraničnej banky","Fio banka, a.s. pobočka zahraničnej banky",
            "mBank S.A, pobočka zahraničnej banky","Oberbank AG pobočka zahraničnej banky v Slovenskej republike",
            "ZUNO BANK AG, pobočka zahraničnej banky"};

            for(int i =0; i < kodBanky.Length; i++)
            {
                if (kodBanky[i].Equals(ucet.code))
                    return nazovBanky[i];
            }
            return null;
        }

    }

    public class Ucet
    {
        public string acc { get; set; }
        public string code { get; set; }
    }    
}