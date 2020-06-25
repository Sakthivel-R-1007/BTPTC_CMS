using Searchs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTPTC.Web
{
    public class SearchResult
    {
        public string Path { get; set; }
        public string Title { get; set; }
    }
    public partial class search : System.Web.UI.Page
    {
        protected Searchs.UserSearch srchSite;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["q"] != null)
            {
                UserSearch sSite = SearchSite(Convert.ToString(Request.QueryString["q"]));
            }

        }

        protected Searchs.UserSearch SearchSite(string strSearch)
        {

            srchSite = new Searchs.UserSearch();
            //Read in all the search words into one variable
            //srchSite.RawSearchWords = srchSite.SearchWords = strSearch;
            srchSite.SearchWords = strSearch;
            srchSite.SearchCriteria = Searchs.SearchCriteria.Phrase;

            //if (Phrase.Checked)
            //    srchSite.SearchCriteria = Searchs.SearchCriteria.Phrase;
            //else
            //    if (AllWords.Checked)
            //        srchSite.SearchCriteria = Searchs.SearchCriteria.AllWords;
            //    else
            //        if (AnyWords.Checked)
            //            srchSite.SearchCriteria = Searchs.SearchCriteria.AnyWords;

            string searchDir = Server.MapPath("~/");

            srchSite.Search(searchDir);   // Search from full site
            DataTable srch = srchSite.PageDataset.Tables[0];


            StringBuilder html = new StringBuilder();
            if (srch.Rows != null)
            {
                foreach (DataRow row in srch.Rows)
                {
                    Dictionary<string, string> routes = new Dictionary<string, string>
                        {
                //About US
                            {"btptc-intro.html","Bishan-Toa Payoh Town Council"},
                            {"our-history.html","Our History"},
                            {"roles-responsibilities.html","Roles & Responsibilities"},
                            {"our-mps.html","Our MPs"},
                            {"personal-data-protection-policy.html","Personal Data Protection Policy"},
               //Our Services      
                            {"service-conservancy-charges.html","Service & Conservancy Charges"},
                            {"bulky-item-removal-services.aspx","Bulky Item Removal Services"},
                            {"booking-rates.html","Common Area & Facilities Booking Rates"},
                            {"handyman-services.html","Handyman Services"},
                            {"modes-of-payment.html","Modes of Payment"},
               //Our Town 
                            {"our-facilities.html","Our Facilities"},
                            {"town-improvement-project.html","Town Improvement Projects"},
                            {"our-events.html","Our Events"},
               //Newsroom 
                            {"newsletter.html","Newsletter"},
                            {"media-release.html","Media Release"},
                            {"annual-report.html","Annual Report"},
                            {"tender.html","Tenders"},
                            {"investment-fund.html","Investment of Town Council's Fund"},
                            {"latest-happenings.html","Latest Happenings"},
               //Gallery 
                            {"gallery.html","Gallery"},
                            {"gallery-2019-tree-planting.html","2019 Tree Planting"},
                            {"gallery-2019-market-visit-and-pal-family-carnival.html","2019 Market Visit And Pal Family Carnival"},
                            {"gallery-2019-shunfu-mart.html","2019 Shunfu Mart"},
                            {"gallery-2019-thomson-sin-ming-celebration.html","2019 Thomson Sin Ming Celebration"},
                            {"gallery-2019-tp-central-r-r-colour-scheme.html","2019 TP Central RR Colour Scheme"},
                            {"gallery-2019-market-visit-toa-payoh.html","2019 Market Visit Toa Payoh"},
                            {"gallery-2018-tree-planting.html","2018 Tree Planting"},
                            {"gallery-2018-tpc-nrp-exhibition.html","2018 TPC NRP Exhibition"},
                            {"gallery-2018-market-visit-bishan.html","2018 Market Visit Bishan"},
                            {"gallery-2018-bn-nrp-exhibition.html","2018 BN NRP Exhibition"},
                            {"gallery-2018-market-blk-93.html","2018 Market BLK 93"},
               //Contact Us 
                            {"our-offices.html","Gallery"},
                            {"sms-alert.aspx","SMS Alert"},
                            {"feedback.aspx","Feedback"},
                            {"faqs.html","FAQs"},
                            {"join-us.html","Join Us"},
                            {"mobile-app.html","Mobile App"},

                            {"index.html","Home"}
                        };

                    try
                    {
                        var data = routes.Where(s => !string.IsNullOrEmpty(s.Key) && s.Key.ToLower().Contains(row[3].ToString().ToLower())).FirstOrDefault();

                        if (!string.IsNullOrEmpty(data.Key) && !string.IsNullOrEmpty(data.Value))
                        {
                            html.Append("<li>");
                            html.Append("<p><strong>" + data.Value + "</strong ></p><p><a href='" + data.Key + "' class='uploadBtn'>Visit Page</a></p>");
                            html.Append("</li>");
                        }
                    }
                    catch (Exception e)
                    {

                    }

                }
            }
            PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            // srchSite.Search(Server.MapPath("~/pdf"));  // Search from pdf folder
            return srchSite;
        }
    }
}