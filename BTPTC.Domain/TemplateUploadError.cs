﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTPTC.Domain
{
    public class TemplateUploadError
    {
        public int Line { get; set; }

        public int Column { get; set; }

        public string Message { get; set; }
    }
}
