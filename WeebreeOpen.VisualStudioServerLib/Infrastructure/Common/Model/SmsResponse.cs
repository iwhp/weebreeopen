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
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Telekom.Common.Model
{
    /// <summary>
    /// Common data in all responses from Telekom service
    /// </summary>
    [DataContract]
    public class SmsResponse : TelekomResponse
    {
        /// <summary>
        /// Returned error status of an operation
        /// </summary>
        [DataMember(Name = "requestError")]
        public SmsResponseStatus requestError { get; set; }

        /// <summary>
        /// Returned successful status of an query report operation
        /// </summary>
        [DataMember(Name = "deliveryInfoList")]
        public SmsResponseStatus deliveryInfoList { get; set; }

        /// <summary>
        /// Returned successful status of an notification subscription operation
        /// </summary>
        [DataMember(Name = "deliveryReceiptSubscription")]
        public SmsResponseStatus deliveryReceiptSubscription { get; set; }

        /// <summary>
        /// Returned successful status of an notification subscription operation
        /// </summary>
        [DataMember(Name = "inboundSMSMessageList")]
        public SmsResponseStatus inboundSMSMessageList { get; set; }

        /// <summary>
        /// Returned successful status of an notification subscription for an sms receive operation
        /// </summary>
        [DataMember(Name = "subscription")]
        public SmsResponseStatus subscription { get; set; }

        /// <summary>
        /// Notification response
        /// </summary>
        [DataMember(Name = "receiveNotificationResponse")]
        public SmsResponseStatus receiveNotificationResponse { get; set; }

        /// <summary>
        /// Returned standard SMS Request response on success
        /// </summary>
        [DataMember(Name = "outboundSMSMessageRequest")]
        public SmsResponseStatus outboundSMSMessageRequest { get; set; }

        /// <summary>
        /// Get the url of the response status and return it as a string
        /// If url is null or regex doesn't find right pattern, returns null
        /// </summary>
        public String GetReportId() 
        {
            String url = this.outboundSMSMessageRequest.resourceURL;
            if (url != null)
            {
                // call Regex.Match.
                Match match = Regex.Match(url, @"requests/([A-Za-z0-9-]{36})|requests/([A-Za-z0-9-]{36})/$", RegexOptions.IgnoreCase);
                string reportId = "";
                if (match.Success)
                {
                    // get the Group value and return it.
                    reportId = match.Groups[1].Value;
                    return reportId;
                }
                else
                {
                    Console.WriteLine("No reportId available");
                    return null;
                }
            }
            else 
            {
                Console.WriteLine("Error - Response URL not set");
                return null;
            }
                
        }

        /// <summary>
        /// Checks if this status represents a successful response
        /// </summary>
        new public bool Success
        {
            get
            {
                return (requestError == null);
            }
        }

    }
}
