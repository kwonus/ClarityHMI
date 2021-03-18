﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QuelleHMI.Definitions
{
    public interface IQuelleSearchControls
    {
        string domain
        {
            get;
        }
        uint span
        {
            get;
        }
        bool exact
        {
            get;
        }
        string host
        {
            get;
        }
    }
    public class CTLSearch: QuelleControlConfig, IQuelleSearchControls
    {
        public const uint maxSpan = 1000;
        public const uint minSpan = 0;
        public const uint defaultSpan = 0;

        public const bool defaultExact = false;

        public string host
        {
            get
            {
                return this.map.ContainsKey("host") ? this.map["host"] : null;
            }
            set
            {
                if (value == null)
                    this.map.Remove("host");
                else
                    this.map["host"] = value;
            }
        }
        public string domain
        {
            get
            {
                return this.map.ContainsKey("domain") ? this.map["domain"] : null;
            }
            set
            {
                if (value == null)
                    this.map.Remove("domain");
                else
                    this.map["domain"] = value;
            }
        }
        public uint span
        {
            get
            {
                string value = this.map.ContainsKey("span") ? this.map["span"] : null;
                if (value == null)
                    return defaultSpan;   // default

                uint val = uint.Parse(value);
                return val >= minSpan && val <= maxSpan ? val : defaultSpan;
            }
            set
            {
                var val = value >= minSpan && value <= maxSpan ? value : defaultSpan;
                this.map["span"] = value.ToString();
            }
        }
        public bool exact
        {
            get
            {
                string value = this.map.ContainsKey("exact") ? this.map["exact"] : null;
                if (value == null)
                    return defaultExact;

                switch (value.ToLower())
                {
                    case "1":
                    case "true":  return true;
                    case "0":
                    case "false": return false;
                    default:      return defaultExact;
                }
            }
            set
            {
                this.map["exact"] = value ? "true" : "false";
            }
        }
        public CTLSearch(string file) : base(file)
        {
            if (!this.map.ContainsKey("host"))
                this.map.Add("host", this.host);
            if (!this.map.ContainsKey("domain"))
                this.map.Add("domain", this.domain);
            if (!this.map.ContainsKey("span"))
                this.map.Add("span", this.span.ToString());
            if (!this.map.ContainsKey("exact"))
                this.map.Add("exact", this.exact ? "true" : "false");
        }
        private CTLSearch(QuelleControlConfig source) : base(source)    // Copy constructor
        {
            ;
        }
        public IQuelleSearchControls CreateCopy
        {
            get
            {
                return new CTLSearch(this);
            }
        }
        public void Update(string host, string domain, uint span, bool exact)
        {
            this.host   = host;
            this.domain = domain;
            this.span   = span;
            this.exact  = exact;
        }
    }
}
