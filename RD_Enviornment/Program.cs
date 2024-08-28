// See https://aka.ms/new-console-template for more information
using RD_Enviornment;
using RD_Enviornment.Constants;
using RD_Enviornment.Data.Dataprocessor;
using RD_Enviornment.Observer;
using RD_Enviornment.Processor;
using RD_Enviornment.Services;
using RD_Enviornment.Test;
using RD_Enviornment.Types;
using System.Security.Cryptography;
using System.Text;

// CreateViesApiLoginCode();
// ObserverTest();
// EncryptionTest();
// ExpressionTest();

await MongoDbTest();

async Task MongoDbTest()
{
    var dataProvider = new DataProvider<TestTable>().InitDataProvider("mongodb://localhost:27017", "local");
    try
    {
        var data = new TestTable()
        {
            _id = "36b3b6c4-f975-466a-b3be-e34fe22b3105",
			Naam = "Jaaa dit is een update",
            Datum = DateTime.Now
        };
        //var testTables = await dataProvider.Find(test => test.Naam == "Dit is deel twee");
        //var testTablesFind = await dataProvider.Find("dfdsfdsf", "Dit is deel twee");
        //var testTablesNumbers = await dataProvider.Find(test => test.Age < 20);
        //await dataProvider.InsertOne(data);
        // await dataProvider.UpdateOne(data);
        var result = await dataProvider.Delete(data);
        await Console.Out.WriteLineAsync($"Deleted: {result}");
    }
	catch (Exception e)
    {
        throw e;
    }
}
void ExpressionTest()
{
    var contract  = new Contract()
    {
        ContractNummer = "555",
        SoortContract = SoortContract.Tao,
    };

    new ExpressionTest<Contract>().ExpressionValidate(contract, contract => contract.ContractNummer == "555");
    new ExpressionTest<Contract>().ExpressionValidate(contract, contract => (int) contract.SoortContract > 5);
}

double GetValueFromFunction(int[] arrayOfNumbers, Func<int[], double> calculator)
{
    var result = calculator.Invoke(arrayOfNumbers);
    return result;

	// GetValueFromFunction(new int[] { 10, 5, 3, 6 }, (i) => { return i.Length * 2.7; });
}

void CreateViesApiLoginCode()
{
    var viesCode = new ViesBtwService().createViesBtwApiHeaderCode("LrNdQm3ok69B", "62dyO0VZaqcZ");
    Console.WriteLine(viesCode);
}
void ObserverTest()
{
    ISubject<int> subject = new Subject<int>();
    subject.SetObject(55);

    var agePrediction = 30;
    var agePrediction2 = 45;

    var observer01 = new Observer<int>().AddSubject(subject).SetAction((agePrediction) => {
        agePrediction = agePrediction + 5;
        Console.WriteLine(agePrediction);
    });

    var observer02 = new Observer<int>().AddSubject(subject).SetAction((agePrediction2) => {
        agePrediction = agePrediction2 * 50;
        Console.WriteLine(agePrediction);
    });

    subject.SetObject(7885);
    subject.Notify();
}
void LinkedListTest()
{
    var headNode = new Node<object>(null, null) { Value = "HeadNode" };
    var secondNode = new Node<object>("secondNode");
    var thirdNode = new Node<object>("thirdNode");

    var linkedList = new LinkedListProcessor<Node<object>>();

    linkedList.Add(headNode);
    linkedList.Add(secondNode);
    linkedList.Add(thirdNode);

    var list = linkedList.ToList();

    foreach (var item in list)
    {
        Console.WriteLine(item.Value);
    }
}

void EncryptionTest()
{
    var testData = System.Text.Encoding.UTF8.GetBytes("Dit is een string value");

    var encryptionService = new EncryptionService();
    encryptionService.SetEncryptionKey("55");

    encryptionService.Encrypt(testData);
    encryptionService.Decrypt(encryptionService.GetEncryptedData());

    var encryptedData = encryptionService.GetEncryptedData();

    Console.WriteLine(System.Text.Encoding.UTF8.GetString(encryptionService.GetEncryptedData()));
    Console.WriteLine(System.Text.Encoding.UTF8.GetString(encryptionService.GetDecryptedData()));
}

void RegexTest()
{
    new RegexTester().IsMatch("aa", "2a").ConsoleThis();
}

void DistinctThis()
{
    var rawData = new string[] { "Apple", "Banana", "Banana", "Apple", "Plum", "Grapes", "Plum" };
    var distinctData = DistinctHelper.Distinct(rawData);

    foreach (var distinct in distinctData)
        Console.WriteLine(distinct);
}

bool HasSameTimeUnit(int currenTimeUnit, int excecuteTimeUnit)
{
    if ((currenTimeUnit < 10) && currenTimeUnit == excecuteTimeUnit) return true;
    if ((currenTimeUnit % excecuteTimeUnit) == 0) return true;

    var tempTime = currenTimeUnit - (Math.Floor((double)currenTimeUnit / 10) * 10);
    if (tempTime == excecuteTimeUnit) return true;

    return false;
}

void EnumGetValues(int monthNumber)
{
    var months = Enum.GetValues(typeof(TimeComponents.Months));
    var monthName = months.GetValue(monthNumber);

    Console.WriteLine(monthName);
}

string GetHmacBtwNoFromVies(string btwNumber, bool isTestApi)
{
    var unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    var nounce = GetNounce();
    var hmacApi = (isTestApi) ? "api-test" : "api";

    // ts + '\n' + nonce + '\n' + method + '\n' + path + '\n' + host + '\n' + port + '\n\n'
    // str = "1574640000\ndt831hs59s\nGET\n/api-test/get/vies/euvat/PL7171642051\nviesapi.eu\n443\n\n"
    var hmacMessage = $@"{unixTime}\{nounce}\nGET\n/{hmacApi}/get/vies/euvat/{btwNumber}\nviesapi.eu\n443\n\n";

    return hmacMessage;
}

string GetNounce()
{
    var byteBuffer = new byte[12];
    new Random().NextBytes(byteBuffer);

    for (int i = 0; i < byteBuffer.Length; i++)
    {
        if (((int) byteBuffer[i] < 65 || (int)byteBuffer[i] > 90) &&
            ((int)byteBuffer[i] < 97 || (int)byteBuffer[i] > 122) &&
            ((int)byteBuffer[i] < 48 || (int)byteBuffer[i] > 57)
            )
            byteBuffer[i] = (byte) ('b' + i);
    }
    return System.Text.UTF8Encoding.UTF8.GetString(byteBuffer);
}

string HMACSHA256(string key, string message)
{
    using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
    {
        byte[] bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));

        StringBuilder builder = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            builder.AppendFormat("{0:x2}", b);
        }
        return builder.ToString();
    }
}

byte[] HMACSHA256Bytes(string key, string message)
{
    using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
    {
        byte[] bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));

        StringBuilder builder = new StringBuilder(bytes.Length * 2);
        foreach (byte b in bytes)
        {
            builder.AppendFormat("{0:x2}", b);
        }
        return System.Text.UTF8Encoding.UTF8.GetBytes(builder.ToString());
    }
}

void GetHmacTest(string key, string hmacMessage)
{
    hmacMessage = @"1574640000\ndt831hs59s\nGET\n/api-test/get/vies/euvat/PL7171642051\nviesapi.eu\n443\n\n";
    key = "test_key";

    var hmac = HMACSHA256(key, hmacMessage);

    Console.WriteLine(hmac);
}

void ToBase64String(string message)
{
    var messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
    var base64String = Convert.ToBase64String(messageBytes);

    Console.WriteLine(base64String);
}