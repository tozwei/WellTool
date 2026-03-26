using WellTool.Json;

namespace WellTool.Json.Tests
{
    public class JSONTest
    {
        [Fact]
        public void TestJSONObject()
        {
            // 测试创建 JSONObject
            var obj = new JSONObject();
            obj["name"] = "John";
            obj["age"] = 30;
            obj["isStudent"] = false;

            // 测试获取值
            Assert.Equal("John", obj.GetString("name"));
            Assert.Equal(30, obj.GetLong("age"));
            Assert.False(obj.GetBoolean("isStudent"));

            // 测试转换为字符串
            var jsonStr = obj.ToString();
            Assert.Contains("name", jsonStr);
            Assert.Contains("John", jsonStr);
            Assert.Contains("age", jsonStr);
            Assert.Contains("30", jsonStr);
            Assert.Contains("isStudent", jsonStr);
            Assert.Contains("false", jsonStr);

            // 测试从字符串解析
            var parsedObj = new JSONObject(jsonStr);
            Assert.Equal("John", parsedObj.GetString("name"));
            Assert.Equal(30, parsedObj.GetLong("age"));
            Assert.False(parsedObj.GetBoolean("isStudent"));
        }

        [Fact]
        public void TestJSONArray()
        {
            // 测试创建 JSONArray
            var arr = new JSONArray();
            arr.Add("John");
            arr.Add(30);
            arr.Add(false);

            // 测试获取值
            Assert.Equal("John", arr.GetString(0));
            Assert.Equal(30, arr.GetLong(1));
            Assert.False(arr.GetBoolean(2));

            // 测试转换为字符串
            var jsonStr = arr.ToString();
            Assert.Contains("John", jsonStr);
            Assert.Contains("30", jsonStr);
            Assert.Contains("false", jsonStr);

            // 测试从字符串解析
            var parsedArr = new JSONArray(jsonStr);
            Assert.Equal("John", parsedArr.GetString(0));
            Assert.Equal(30, parsedArr.GetLong(1));
            Assert.False(parsedArr.GetBoolean(2));
        }

        [Fact]
        public void TestJSONUtil()
        {
            // 测试解析 JSON 字符串
            var jsonStr = "{\"name\":\"John\",\"age\":30,\"isStudent\":false}";
            var obj = JSONUtil.ParseObject(jsonStr);
            Assert.Equal("John", obj.GetString("name"));
            Assert.Equal(30, obj.GetLong("age"));
            Assert.False(obj.GetBoolean("isStudent"));

            // 测试格式化 JSON
            var formattedStr = JSONUtil.Format(obj);
            Assert.Contains("name", formattedStr);
            Assert.Contains("John", formattedStr);
            Assert.Contains("age", formattedStr);
            Assert.Contains("30", formattedStr);
            Assert.Contains("isStudent", formattedStr);
            Assert.Contains("false", formattedStr);

            // 测试将对象转换为 JSON
            var testObj = new JSONObject();
            testObj["name"] = "Jane";
            testObj["age"] = 25;
            testObj["isStudent"] = true;
            var jsonFromObj = JSONUtil.ToJson(testObj);
            Assert.Contains("name", jsonFromObj);
            Assert.Contains("Jane", jsonFromObj);
            Assert.Contains("age", jsonFromObj);
            Assert.Contains("25", jsonFromObj);
            Assert.Contains("isStudent", jsonFromObj);
            Assert.Contains("true", jsonFromObj);
        }

        [Fact]
        public void TestNestedJSON()
        {
            // 测试嵌套 JSON
            var obj = new JSONObject();
            obj["name"] = "John";
            obj["age"] = 30;

            var address = new JSONObject();
            address["street"] = "123 Main St";
            address["city"] = "New York";
            obj["address"] = address;

            var hobbies = new JSONArray();
            hobbies.Add("reading");
            hobbies.Add("swimming");
            obj["hobbies"] = hobbies;

            // 测试获取嵌套值
            Assert.Equal("123 Main St", obj.GetJSONObject("address").GetString("street"));
            Assert.Equal("New York", obj.GetJSONObject("address").GetString("city"));
            Assert.Equal("reading", obj.GetJSONArray("hobbies").GetString(0));
            Assert.Equal("swimming", obj.GetJSONArray("hobbies").GetString(1));

            // 测试转换为字符串
            var jsonStr = obj.ToString();
            Assert.Contains("name", jsonStr);
            Assert.Contains("John", jsonStr);
            Assert.Contains("age", jsonStr);
            Assert.Contains("30", jsonStr);
            Assert.Contains("address", jsonStr);
            Assert.Contains("street", jsonStr);
            Assert.Contains("123 Main St", jsonStr);
            Assert.Contains("city", jsonStr);
            Assert.Contains("New York", jsonStr);
            Assert.Contains("hobbies", jsonStr);
            Assert.Contains("reading", jsonStr);
            Assert.Contains("swimming", jsonStr);

            // 测试从字符串解析
            var parsedObj = new JSONObject(jsonStr);
            Assert.Equal("123 Main St", parsedObj.GetJSONObject("address").GetString("street"));
            Assert.Equal("New York", parsedObj.GetJSONObject("address").GetString("city"));
            Assert.Equal("reading", parsedObj.GetJSONArray("hobbies").GetString(0));
            Assert.Equal("swimming", parsedObj.GetJSONArray("hobbies").GetString(1));
        }
    }
}