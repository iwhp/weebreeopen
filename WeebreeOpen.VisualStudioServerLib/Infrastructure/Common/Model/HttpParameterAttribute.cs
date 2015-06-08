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

namespace Telekom.Common.Model
{
    /// <summary>
    /// Properties are serialized by default. Use this attribute to
    /// overwrite default (inferred) name
    /// </summary>
    public class HttpParameterAttribute : Attribute
    {
        /// <summary>
        /// Overwrite name
        /// </summary>
        /// <param name="name">custom name</param>
        internal HttpParameterAttribute(string name)
        {
            Name = name;
        }

        internal string Name { get; set; }
    }
}
