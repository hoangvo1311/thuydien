using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Converters
{
    public class ContentConverter : JsonConverter
    {
        #region Public Methods

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(NoiDung));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var content = (NoiDung)value;
            JArray valueArray = new JArray();
            valueArray.Add(content.KyHieuTram);
            valueArray.Add(content.ThongSoDo);
            valueArray.Add(content.GiaTriDo);
            valueArray.Add(content.DonViTinh);
            valueArray.Add(content.ThoiGianDo);
            valueArray.Add(content.TrangThaiDo);
            valueArray.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
