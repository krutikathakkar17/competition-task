using MarsEducationCertificationsSpecflow.JSONDataObject;
using MarsEducationCertificationsTests.JSONDataObject;
using Newtonsoft.Json;

namespace MarsEducationCertificationsSpecflow.Utilities
{
    public class JsonDataReader
    {
        //private readonly string filePath;
        private readonly string _sampleJsonFilePath;
        public JsonDataReader(string sampleJsonFilePath)
        {
            _sampleJsonFilePath = sampleJsonFilePath;
        }


        public List<AddCertification> ReadCertificationFile()
        {
            using StreamReader reader = new(_sampleJsonFilePath);
            var json = reader.ReadToEnd();
            List<AddCertification> list = JsonConvert.DeserializeObject<List<AddCertification>>(json);
            return list;

        }


        public List<AddDuplicateCertification> ReadDuplicateCertification()
        {
            using StreamReader reader = new(_sampleJsonFilePath);
            var json = reader.ReadToEnd();
            List<AddDuplicateCertification> list = JsonConvert.DeserializeObject<List<AddDuplicateCertification>>(json);
            return list;

        }

        public List<EditCertification> ReadUpdateCeritificationFile()
        {
            using StreamReader reader = new(_sampleJsonFilePath);
            var json = reader.ReadToEnd();
            List<EditCertification> list = JsonConvert.DeserializeObject<List<EditCertification>>(json);
            return list;

        }
        public List<DeleteCertification> ReadDeleteCertificationFile()
        {
            using StreamReader reader = new(_sampleJsonFilePath);
            var json = reader.ReadToEnd();
            List<DeleteCertification> list = JsonConvert.DeserializeObject<List<DeleteCertification>>(json);
            return list;

        }

        public List<AddEducation> ReadAddEducationFile()
        {
            using StreamReader reader = new(_sampleJsonFilePath);
            var json = reader.ReadToEnd();
            List<AddEducation> list = JsonConvert.DeserializeObject<List<AddEducation>>(json);
            return list;

        }

        public List<AddEmptyEducation> ReadAddEmptyEducationFile()
        {
            using StreamReader reader = new(_sampleJsonFilePath);
            var json = reader.ReadToEnd();
            List<AddEmptyEducation> list = JsonConvert.DeserializeObject<List<AddEmptyEducation>>(json);
            return list;

        }


        public List<EditEducation> ReadEditEducationFile()
        {
            using StreamReader reader = new(_sampleJsonFilePath);
            var json = reader.ReadToEnd();
            List<EditEducation> list = JsonConvert.DeserializeObject<List<EditEducation>>(json);
            return list;

        }

        public List<DeleteEducation> ReadDeleteEducationFile()
        {
            using StreamReader reader = new(_sampleJsonFilePath);
            var json = reader.ReadToEnd();
            List<DeleteEducation> list = JsonConvert.DeserializeObject<List<DeleteEducation>>(json);
            return list;

        }


    }


}




    

