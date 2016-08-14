using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Controllers;
using RestApi.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace RestApi.Tests
{
    [TestClass]
    public class TestPatientController
    {
        private IPatientContext InitialiseTestData()
        {
            var patientContext = new TestPatientContext();

            patientContext.Patients.Add(new Patient
            {
                DateOfBirth = new DateTime(1972, 10, 27),
                FirstName = "Millicent",
                PatientId = 1,
                LastName = "Hammond",
                NhsNumber = "1111111111"
            });

            patientContext.Patients.Add(new Patient
            {
                DateOfBirth = new DateTime(1975, 1, 22),
                FirstName = "Jon",
                PatientId = 2,
                LastName = "Hammond",
                NhsNumber = "1111111112"
            });

            patientContext.Episodes.Add(new Episode
            {
                AdmissionDate = new DateTime(2014, 11, 12),
                Diagnosis = "Irritation of inner ear",
                DischargeDate = new DateTime(2014, 11, 27),
                EpisodeId = 1,
                PatientId = 1
            });

            return patientContext;
        }

        [TestMethod]
        public void TestCorrectPatientIsReturnedWithTheCorrectEpisode()
        {
            var patientContext = InitialiseTestData();
            var controller = new PatientsController(patientContext);
            var patient = controller.Get(1);

            Assert.IsNotNull(patient);
            Assert.AreEqual(1, patient.PatientId);

            var episode = patient.Episodes.FirstOrDefault();
            Assert.IsNotNull(episode);
            Assert.AreEqual(1, episode.EpisodeId);
            Assert.AreEqual(1, patient.Episodes.Count());
        }

        [TestMethod]
        public void TestIfPatientIsNotFoundA404ExceptionIsThrown()
        {
            HttpResponseException expectedException = null;

            var patientContext = InitialiseTestData();
            var controller = new PatientsController(patientContext);

            try
            {
                var patient = controller.Get(3);
            }
            catch(HttpResponseException ex)
            {
                expectedException = ex;
            }

            Assert.IsNotNull(expectedException);
            Assert.AreEqual(HttpStatusCode.NotFound, expectedException.Response.StatusCode);
        }

        [TestMethod]
        public void TestIfPatientHasNoEpisodesA404ExceptionIsThrown()
        {
            HttpResponseException expectedException = null;

            var patientContext = InitialiseTestData();
            var controller = new PatientsController(patientContext);

            try
            {
                var patient = controller.Get(2);
            }
            catch (HttpResponseException ex)
            {
                expectedException = ex;
            }

            Assert.IsNotNull(expectedException);
            Assert.AreEqual(HttpStatusCode.NotFound, expectedException.Response.StatusCode);
        }
    }
}