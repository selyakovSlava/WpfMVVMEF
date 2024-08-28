using System.IO;
using System.Net.Http;
using System.Xml.Linq;

namespace WPFExampleEF.Parsing
{
    /// <summary>
    /// Запрос к API Мосбиржи и парсинг ответа.
    /// </summary>
    public class BondParsing
    {
        /// <summary>
        /// Шаблон строки запроса к API.
        /// </summary>
        private readonly string _request = "https://iss.moex.com/iss/engines/stock/markets/bonds/boards/TQOB/securities/{0}.xml?iss.meta=off";

        /// <summary>
        /// Строка запроса к API.
        /// </summary>
        //private readonly string _request;
        
        /// <summary>
        /// Ответ от API Мосбиржи.
        /// </summary>
        private HttpResponseMessage _responseMessage;

        /// <summary>
        /// Ответ от API в XML.
        /// </summary>
        private XDocument _document;

        /// <summary>
        /// Узел data для securities.
        /// </summary>
        private IEnumerable<XElement> _nodeDataSecurities;

        /// <summary>
        /// Строка для securities.
        /// </summary>
        private XElement _nodeRowOfDataSecurities;

        /// <summary>
        /// Узел data для marketdata_yields.
        /// </summary>
        private IEnumerable<XElement> _nodeMarketDataYields;

        /// <summary>
        /// Строка для marketdata_yields.
        /// </summary>
        private XElement _nodeRowOfMarketDataYields;

        public BondParsing(string identificator)
        {
            _request = String.Format(_request, identificator);

            Load();
            
        }

        /// <summary>
        /// Отправка запроса к API и получение ответа.
        /// </summary>
        private void Load()
        {
            Task.Factory.StartNew(() =>
            {
                GetAnswer();
                
            }).Wait();

            GetNodes();
        }
        
        
        /// <summary>
        /// Получение ответа от МосБиржи по запросу и загрузка в XML.
        /// </summary>
        /// <param name="request">Строка запроса API.</param>
        private async Task GetAnswer()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    _responseMessage = client.GetAsync(_request).Result;

                    if ((_responseMessage as HttpResponseMessage).IsSuccessStatusCode)
                    {
                        var xml = await (_responseMessage as HttpResponseMessage).Content.ReadAsStringAsync();

                        using (TextReader tr = new StringReader(xml))
                        {
                            _document = XDocument.Load(tr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Получение необходимых узлов ответа API.
        /// </summary>
        private void GetNodes()
        {
            if (_document != null)
            {
                XElement booksElement = _document.Element("document");
                IEnumerable<XElement> bookElements = booksElement.Elements("data");

                _nodeDataSecurities = bookElements.FirstOrDefault(p => p.Attribute("id")?.Value == "securities").Elements("rows");
                _nodeMarketDataYields = bookElements.FirstOrDefault(p => p.Attribute("id")?.Value == "marketdata_yields").Elements("rows");
            }

            if (_nodeDataSecurities != null)
            {
                _nodeRowOfDataSecurities = _nodeDataSecurities.First();
            }

            if (_nodeMarketDataYields != null)
            {
                _nodeRowOfMarketDataYields = _nodeMarketDataYields.First();
            }
        }

        /// <summary>
        /// Получить имя бумаги.
        /// </summary>
        /// <returns>Имя бумаги.</returns>
        public string GetName()
        {
            string result = String.Empty;

            if (_nodeRowOfDataSecurities != null)
            {
                result = (_nodeRowOfDataSecurities.LastNode as XElement).Attribute("SHORTNAME").Value; //.SelectSingleNode("@SHORTNAME").Value;
            }

            return result;
        }

        /// <summary>
        /// Получить значение купона.
        /// </summary>
        /// <returns>Значение купона.</returns>
        public double GetCouponValue()
        {
            double result = 0;

            if (_nodeRowOfDataSecurities != null)
            {
                result = double.Parse((_nodeRowOfDataSecurities.LastNode as XElement).Attribute("COUPONVALUE").Value, System.Globalization.CultureInfo.InvariantCulture);
            }

            return result;
        }

        /// <summary>
        /// Получить НКД.
        /// </summary>
        /// <returns>НКД.</returns>
        public double GetAccumulatedCouponIncome()
        {
            double result = 0;

            if (_nodeRowOfDataSecurities != null)
            {
                result = double.Parse((_nodeRowOfDataSecurities.LastNode as XElement).Attribute("ACCRUEDINT").Value, System.Globalization.CultureInfo.InvariantCulture);
            }

            return result;
        }

        /// <summary>
        /// Получить дату следующего купона.
        /// </summary>
        /// <returns>
        /// Дата следующего купона.
        /// Если дата не получена, то возвращается текущие дата/время.
        /// </returns>
        public DateTime GetDateNextCoupon()
        {
            DateTime result = DateTime.Now;

            if (_nodeRowOfDataSecurities != null)
            {
                result = DateTime.Parse((_nodeRowOfDataSecurities.LastNode as XElement).Attribute("NEXTCOUPON").Value);
            }

            return result;
        }

        /// <summary>
        /// Получить дату закрытия бумаги.
        /// </summary>
        /// <returns>
        /// Дата закрытия.
        /// Если дата не получена, то возвращается текущие дата/время.
        /// </returns>
        public DateTime GetDateClose()
        {
            DateTime result = DateTime.Now;

            if (_nodeRowOfDataSecurities != null)
            {
                result = DateTime.Parse((_nodeRowOfDataSecurities.LastNode as XElement).Attribute("MATDATE").Value);
            }

            return result;
        }

        /// <summary>
        /// Получить текущую цену.
        /// </summary>
        /// <returns>Цена.</returns>
        public double GetCurrentPrice()
        {
            double result = 0;

            if (_nodeRowOfMarketDataYields != null)
            {
                result = double.Parse((_nodeRowOfMarketDataYields.LastNode as XElement).Attribute("PRICE").Value, System.Globalization.CultureInfo.InvariantCulture);
            }

            return result * 10;
        }
    }
}
