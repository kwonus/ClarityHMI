﻿using System;
using System.Net.Http;
using QuelleHMI;
using QuelleHMI.Definitions;
using QuelleHMI.Actions;

//  This driver implementation depends upon QuelleHMI library
//
//  The source code here, illustrates the simplicity of creating a Quelle driver using the Quelle-HMI library.
//  This code can be used as a template to create your own driver, or it can be subclassed to extend behavior:
//  Specifically, the Cloud*() methods need implementations in the subclass to provide the actual search functionality.
//
//  A tandem project in github provides an interpreter.  Between these two projects, there are less than 500 
//  lines of code to extend and/or modify.  The value proposition here is that parsing is tedious. And starting
//  your search CLI with a concise syntax with an easy to digest parsing library could easily save your team
//  a person-year in design-time and coding.  Quelle* source code is licensed with an MIT/BSD-style license.
//  The specific licence file will be added to the github projects shortly (perhaps it is already here).
//
//  The design incentive for QuelleHMI and the standard driver is to support the broader Digital-AV effort:
//  That project [Digital-AV] provides a command-line interface for searching and publishing the KJV bible.
//  Every attempt has been made to keep the Quelle syntax agnostic about the search domain, yet the Quelle
//  user documentation itself [coming soon to github] is heavily biased in its syntax examples. Still, the search
//  domain of the Standard Quelle Driver remains unbiased.

namespace Quelle.DriverDefault
{    public class QuelleDriver : IQuelleHelp
    {
        public QuelleDriver()
        {
            ;
        }

        public string Help()
        {
            string text = "Help is available on each of these topics:\n";

            foreach (var verb in new string[] { QuelleHMI.Actions.Search.FIND, Control.SET, Control.CLEAR, Label.SAVE, Label.DELETE, Label.SHOW, Display.PRINT, Control_Get.GET, Search_Status.STATUS, QuelleHMI.Actions.System.GENERATE, QuelleHMI.Actions.System.REGENERATE, "@exit" })
                text += ("\n\t" + (verb.StartsWith("@") ? verb.Substring(1) : verb));

            text += "\n\n";
            text += "For extensive extensive help, consult the project README:\n";
            text += "https://github.com/kwonus/Quelle/blob/master/Quelle.md";

            return text;
        }
        public string Help(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
                return this.Help();

            //  This block makes the @ optional on all verbs
            var help = topic.Trim().ToLower();
            if (help.StartsWith("@"))
                help = help.Substring(1).Trim();
            foreach (var verb in new string[] { QuelleHMI.Actions.Search.FIND, Control.SET, Control.CLEAR, Display.PRINT, Label.SAVE, Label.DELETE, Label.SHOW, Control_Get.GET, Search_Status.STATUS, QuelleHMI.Actions.System.GENERATE, QuelleHMI.Actions.System.REGENERATE, "@exit" })
                if (verb.EndsWith(help) && verb.StartsWith("@"))
                {
                    help = '@' + help;
                    break;
                }

            // Look for verbs & topics
            if (Search.IMPLICIT.Contains(help) || Search.SYNTAX.Equals(help, StringComparison.InvariantCultureIgnoreCase))
                return Search.Help(help);
            if (Search_Status.EXPLICIT.Contains(help))
                return Control_Get.Help();

            if (Display.EXPLICIT.Contains(help) || Display.SYNTAX.Equals(help, StringComparison.InvariantCultureIgnoreCase))
                return Display.Help(help);

            if (Label.EXPLICIT.Contains(help) || Label.SYNTAX.Equals(help, StringComparison.InvariantCultureIgnoreCase))
                return Label.Help(help);

            if (Control.IMPLICIT.Contains(help) || Control.SYNTAX.Equals(help, StringComparison.InvariantCultureIgnoreCase))
                return Control.Help(help);
            if (Control_Get.EXPLICIT.Contains(help))
                return Control_Get.Help();

            if (QuelleHMI.Actions.System.EXPLICIT.Contains(help) || QuelleHMI.Actions.System.SYNTAX.Equals(help, StringComparison.InvariantCultureIgnoreCase))
                return QuelleHMI.Actions.System.Help(help);

            if (QuelleHMI.Actions.History.EXPLICIT.Contains(help) || QuelleHMI.Actions.History.SYNTAX.Equals(help, StringComparison.InvariantCultureIgnoreCase))
                return QuelleHMI.Actions.History.Help(help);

            return this.Help();
        }
    }
}
