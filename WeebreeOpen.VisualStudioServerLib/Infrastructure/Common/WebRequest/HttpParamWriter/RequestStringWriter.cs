// This file is part of the Telekom .NET SDK
// Copyright 2010 Deutsche Telekom AG
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Telekom.Common.WebRequest.HttpParamWriter
{
    /// <summary>
    /// Writes parameters to body in request string format
    /// </summary>
    internal class RequestStringWriter : HttpBodyParamWriter
    {
        private bool first = true;

        public RequestStringWriter(Stream stream)
            : base(stream)
        {
        }

        public override void WriteParam(string name, string value)
        {
            // Parameter delimination
            if (first)
            {
                first = false;
            }
            else
            {
                writer.Write('&');
            }

            // Use custom UriHelper class because of length-limitation of .NET Uri class
            writer.Write("{0}={1}", UriHelper.EscapeDataString(name), UriHelper.EscapeDataString(value));
        }

        public override void WriteFile(string name, Stream value)
        {
            // This is not supported, files are sent using form/multipart writer
            throw new InvalidOperationException();
        }

        public override string GetContentType()
        {
            return "application/x-www-form-urlencoded";
        }

    }
}
