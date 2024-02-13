using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ERIP.Services
{
    public class HashService
    {
        // https://stackoverflow.com/questions/17292366/hashing-with-sha1-algorithm-in-c-sharp
        public static string Sha1(string input)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }

        public static string MD5(string s)
        {
            using var provider = System.Security.Cryptography.MD5.Create();
            StringBuilder builder = new StringBuilder();

            foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }

        public static void MockExamples()
        {
            var wsb_seed = 1242649174;
            var wsb_storeid = 11111111;
            var wsb_order_num = "ORDER-12345678";
            var wsb_test = 1;
            var wsb_currency_id = "BYN";
            var wsb_total = 21.90;
            var SecretKey = "12345678901234567890";

            // Значение объединенной строки:
            // 124264917411111111ORDER-123456781BYN21.9012345678901234567890
            // для версии протокола 2 (wsb_version = 2)
            var wsbV2Input = $"{wsb_seed}{wsb_storeid}{wsb_order_num}{wsb_test}{wsb_currency_id}{wsb_total.ToString("#.00")}{SecretKey}";
            var wsb_signature = ERIP.Services.HashService.Sha1(wsbV2Input);
            var equals = wsb_signature == "338d1647833079f9353907ad266ec0bb5264c0d9";
            //// 338d1647833079f9353907ad266ec0bb5264c0d9

            System.Diagnostics.Debug.WriteLine(wsb_signature);

            // если версия не указана
            var wsbInput = $"{wsb_seed}{wsb_storeid}{wsb_order_num}{wsb_test}{wsb_currency_id}{wsb_total.ToString("#.00")}{SecretKey}";
            var wsb_signature2 = ERIP.Services.HashService.MD5(wsbInput);
            var equals2 = wsb_signature2 == "5b712daa1743d1a62dfdb7054b3978a1";
            // 5b712daa1743d1a62dfdb7054b3978a1
        }

        public static string RealExamples()
        {
            var wsb_seed = 1242649174;
            var wsb_storeid = 11111111;
            var wsb_order_num = "ORDER-12345678";
            var wsb_test = 1;
            var wsb_currency_id = "BYN";
            var wsb_total = 21.90;
            var SecretKey = "12345678901234567890";

            // Значение объединенной строки:
            // 124264917411111111ORDER-123456781BYN21.9012345678901234567890
            // для версии протокола 2 (wsb_version = 2)
            var wsbV2Input = $"{wsb_seed}{wsb_storeid}{wsb_order_num}{wsb_test}{wsb_currency_id}{wsb_total.ToString("#.00")}{SecretKey}";
            var wsb_signature = ERIP.Services.HashService.Sha1(wsbV2Input);
            var equals = wsb_signature == "338d1647833079f9353907ad266ec0bb5264c0d9";
            //// 338d1647833079f9353907ad266ec0bb5264c0d9

            System.Diagnostics.Debug.WriteLine(wsb_signature);

            // если версия не указана
            var wsbInput = $"{wsb_seed}{wsb_storeid}{wsb_order_num}{wsb_test}{wsb_currency_id}{wsb_total.ToString("#.00")}{SecretKey}";
            var wsb_signature2 = ERIP.Services.HashService.MD5(wsbInput);
            var equals2 = wsb_signature2 == "5b712daa1743d1a62dfdb7054b3978a1";
            // 5b712daa1743d1a62dfdb7054b3978a1

            return wsb_signature;
        }
    }
}
