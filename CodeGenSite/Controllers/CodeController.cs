namespace CodeGenSite.Controllers
{
    using System.Web.Mvc;
    using Logic;
    using Models;
    using Validation = Logic.Entity.Validation;
    using Extensions;
    using System.Threading.Tasks;

    public class CodeController : AsyncController
    {
        #region Index

        [CompressFilter]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "CodeGenerate.me - Generate free QR codes and Barcodes online.";

            return View();
        }

        #endregion

        #region About

        [CompressFilter]
        public ActionResult About()
        {
            ViewBag.PageTitle = "CodeGenerate.me - A little bit more information about our free and efficient barcode and QR code generating service.";

            return View();
        }

        #endregion

        #region Saved
        [CompressFilter]
        public ActionResult Saved()
        {
           ViewBag.PageTitle = "CodeGenerate.me - A list of your previously generated codes.";
            return View();
        }

        #endregion

        #region Contact

        [CompressFilter]
        public ActionResult Contact()
        {
            ViewBag.PageTitle = 
                "CodeGenerate.me - Contact us.";

            return View(new ContactModel {SentSuccesfully = 1});
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contactModel)
        {
            ViewBag.PageTitle = 
                "CodeGenerate.me - Contact us.";

            if (ModelState.IsValid)
            {
                SendEmail sendEmail = new SendEmail(contactModel.Email, contactModel.Subject, contactModel.Description,
                                                    contactModel.Name);

                // Successful
                contactModel.SentSuccesfully = sendEmail.SentSuccessfully;
            }
            else
            {

                // Not ready - validation failed
                contactModel.SentSuccesfully = 1;
            }

            return View(contactModel);
        }

        #endregion

        #region API
        [CompressFilter]
        public ActionResult API()
        {
            ViewBag.PageTitle = 
                "CodeGenerate.me - Our new API is currently in development.";

            return View(new ContactModel {SentSuccesfully = 1});
        }

        [HttpPost]
        public ActionResult API(ContactModel contactModel)
        {
            ViewBag.PageTitle = 
                "CodeGenerate.me - Our new API is currently in development.";

            if (ModelState.IsValid)
            {
                SendEmail sendEmail = new SendEmail(contactModel.Email, contactModel.Subject, contactModel.Description,
                                                    contactModel.Name);

                // Successful
                contactModel.SentSuccesfully = 2;
            }
            else
            {

                // Not ready - validation failed
                contactModel.SentSuccesfully = 1;
            }

            return View(contactModel);
        }

        #endregion

        #region Validation

        public ActionResult Validate(string type, string value)
        {
            ValidateInput validation = new ValidateInput();
            Validation validationResult = validation.ValidateBarcodeValue(type, value);

            return Json(validationResult, JsonRequestBehavior.AllowGet);
        }


        public void BarcodeAsync(string type, string value)
        {
            AsyncManager.OutstandingOperations.Increment(1);

            Task.Factory.StartNew(() =>
            {
                Barcode barcode = new Barcode();
                var generate128 = barcode.GenerateBarcode(type, value);

                AsyncManager.Parameters["imageBytes"] = generate128.ImageBytes;
                AsyncManager.OutstandingOperations.Decrement();
            });
                
        }

        public ActionResult BarcodeCompleted(byte[] imageBytes)
        {
            return File(imageBytes, "image/png");
        }

        #endregion

        #region AppView
        [CompressFilter]
        public ActionResult AppView()
        {
            ViewBag.PageTitle = "CodeGenerate.me - Generate free QR codes and Barcodes online.";

            return View();
        }
        #endregion

        #region Manifest

        public ActionResult Manifest()
        {
            ManifestModel manifestModel = new ManifestModel
                                              {
                                                  ManifestString =
                                                      "{\"version\": \"1.0\"," +
                                                      "\"name\": \"CodeGenerate\"," +
                                                      "\"description\": \"Generate free QR codes and Barcodes online!\"," +
                                                      "\"icons\": {\"16\": \"/cdn/16x16.png\",\"48\": \"/cdn/48x48.png\",\"128\": \"/cdn/128x128.png\"}," +
                                                      "\"developer\": {\"name\": \"Dean Hume\",\"url\": \"http://www.deanhume.com\"},\"installs_allowed_from\": [\"*\"]," +
                                                      "\"locales\": {\"de\": {\"description\": \"Generieren Sie gratis QR-Codes und Barcodes online!\"," +
                                                      "\"developer\": {\"url\": \"http://www.deanhume.com\"}}}," +
                                                      "\"default_locale\": \"en\"}"
                                              };

            return Content(manifestModel.ManifestString, "application/x-web-app-manifest+json");
        }

        #endregion
    }
}
