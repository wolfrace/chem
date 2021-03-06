﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chem.Models;
using Chem.Managers;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Web.Mvc;
using Chem.Models.Search;
using System.Web;
using Common.Helpers;
using System.Text.RegularExpressions;

namespace Chem.Controllers
{
    
    //[Authorize]
    public class SubstancesController : ApiController
    {
        SubstancesService _service = new SubstancesService();

        // GET api/values
        public List<SubstancePreview> GetByQuery([FromUri] string query)
        {
            var q = JsonConvert.DeserializeObject<QueryModel>(query);
            return _service.GetByQuery(q);
        }

        // GET api/values/5
        public FullSubstanceModel Get(int id)
        {
            return _service.GetById(id);
        }

        // GET api/values/5
        public List<Reaction> GetReactionList(int id)
        {
            var elem = _service.GetById(id);
            var formula = elem.Formula;
            var isOrganic = elem.Categories.Any(x => x.ToLower() == "органическое вещество");
            var f = LoadPageService.LoadReactionList(formula, isOrganic);
            if (f == null) return null;
            return f.Take(100).ToList();
        }
    }
}
