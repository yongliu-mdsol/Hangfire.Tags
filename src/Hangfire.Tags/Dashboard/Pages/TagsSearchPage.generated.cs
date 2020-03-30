﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hangfire.Tags.Dashboard.Pages
{
    
    #line 2 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
    using System;
    
    #line default
    #line hidden
    using System.Collections.Generic;
    
    #line 3 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 4 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
    using System.Text.RegularExpressions;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
    using Hangfire.Dashboard;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
    using Hangfire.Dashboard.Pages;
    
    #line default
    #line hidden
    
    #line 7 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
    using Hangfire.Dashboard.Resources;
    
    #line default
    #line hidden
    
    #line 8 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
    using Hangfire.Tags.Storage;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class TagsSearchPage : RazorPage
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");










            
            #line 10 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
  
    Layout = new LayoutPage("Tags");

    int.TryParse(Query("from"), out var from);
    int.TryParse(Query("count"), out var perPage);

    var tags = new string[0];
    string state = null;

    var match = Regex.Match(RequestPath, "^/tags/search(/(?<tags>[^/]+))(/(?<state>[^/]+))?");
    if (match.Success)
    {
        tags = match.Groups["tags"].Value.Split(',');
        state = match.Groups["state"].Value;
    }

    var tagStorage = new TagsStorage(Storage);
    var monitor = tagStorage.GetMonitoringApi();


            
            #line default
            #line hidden
WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col-md-3\">\r\n        ");


            
            #line 32 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
   Write(Html.JobsSidebar());

            
            #line default
            #line hidden
WriteLiteral("\r\n    </div>\r\n    <div class=\"col-md-9\">\r\n");


            
            #line 35 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
         if (tags.Length == 1)
        {

            
            #line default
            #line hidden
WriteLiteral("            <h1 class=\"page-header\">Tag ");


            
            #line 37 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                   Write(tags[0]);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n");


            
            #line 38 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
        }
        else if (tags.Length > 1)
        {

            
            #line default
            #line hidden
WriteLiteral("            <h1 class=\"page-header\">Tags ");


            
            #line 41 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                    Write(string.Join(", ", tags));

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n");


            
            #line 42 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
        }
        else
        {

            
            #line default
            #line hidden
WriteLiteral("            <h1 class=\"page-header\">Tags</h1>\r\n");


            
            #line 46 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("        <!-- Add search box here -->\r\n        <div class=\"js-search\">\r\n        </" +
"div>\r\n\r\n");


            
            #line 51 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
          
            if (tags.Length == 0)
            {
                // Show a page with all tags
                var allTags = monitor.SearchWeightedTags().ToList();

                if (!allTags.Any())
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div class=\"tags\">\r\n                        There are no tags" +
" found yet.\r\n                    </div>\r\n");


            
            #line 62 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                }
                else
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div class=\"tags\">\r\n                        <datalist id=\"myt" +
"ags\">\r\n");


            
            #line 67 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                             foreach (var t in allTags)
                            {

            
            #line default
            #line hidden
WriteLiteral("                                <option value=\"");


            
            #line 69 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                          Write(t.Tag);

            
            #line default
            #line hidden
WriteLiteral("\" />\r\n");


            
            #line 70 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                            }

            
            #line default
            #line hidden
WriteLiteral("                        </datalist>\r\n                        <input class=\"form-c" +
"ontrol\" width=\"400px\" id=\"selectedTag\" list=\"mytags\" multiple placeholder=\"selec" +
"t a tag\">\r\n\r\n                        <button class=\"btn\" onclick=\"location.href " +
"= \'");


            
            #line 74 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                                 Write(Url.To("/tags/search/"));

            
            #line default
            #line hidden
WriteLiteral("\' + document.getElementById(\'selectedTag\').value\">Search</button>\r\n              " +
"      </div>\r\n");


            
            #line 76 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                }
            }
            else
            {
                // Show a page with a list of matching jobs

                var pager = new Pager(from, perPage, monitor.GetJobCount(tags, state));
                var matchingJobs = monitor.GetMatchingJobs(tags, pager.FromRecord, pager.RecordsPerPage, state);

                if (pager.TotalPageCount == 0)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div class=\"alert alert-info\">\r\n                        There" +
" are no jobs found for the selected tag(s).\r\n                    </div>\r\n");


            
            #line 90 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                }
                else
                {
                    var matchingStates = monitor.GetJobStateCount(tags);
                    if (matchingStates.Any())
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <div class=\"js-state-list\">\r\n                            " +
"<div class=\"btn-toolbar btn-toolbar-top\">\r\n");


            
            #line 98 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                 foreach (var matchingState in matchingStates)
                                {
                                    var css = state == matchingState.Key ? "btn-primary" : "";

            
            #line default
            #line hidden
WriteLiteral("                                    <a role=\"button\" href=\"");


            
            #line 101 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                      Write(Url.To("/tags/search/" + string.Join(",", tags) + "/" + matchingState.Key));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"js-state-list-command btn btn-sm ");


            
            #line 101 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                                                                                                                                           Write(css);

            
            #line default
            #line hidden
WriteLiteral("\">");


            
            #line 101 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                                                                                                                                                 Write(matchingState.Key);

            
            #line default
            #line hidden
WriteLiteral(" <span class=\"badge\">");


            
            #line 101 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                                                                                                                                                                                        Write(matchingState.Value);

            
            #line default
            #line hidden
WriteLiteral("</span></a>\r\n");


            
            #line 102 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                }

            
            #line default
            #line hidden
WriteLiteral("                            </div>\r\n                        </div>\r\n");


            
            #line 105 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                    }


            
            #line default
            #line hidden
WriteLiteral("                    <div class=\"js-jobs-list\">\r\n                        <div clas" +
"s=\"btn-toolbar btn-toolbar-top\">\r\n                            <button class=\"js-" +
"jobs-list-command btn btn-sm btn-primary\"\r\n                                    d" +
"ata-url=\"");


            
            #line 110 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                         Write(Url.To("/jobs/enqueued/requeue"));

            
            #line default
            #line hidden
WriteLiteral("\"\r\n                                    data-loading-text=\"");


            
            #line 111 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                  Write(Strings.Common_Enqueueing);

            
            #line default
            #line hidden
WriteLiteral("\"\r\n                                    disabled=\"disabled\">\r\n                    " +
"            <span class=\"glyphicon glyphicon-repeat\"></span>\r\n                  " +
"              ");


            
            #line 114 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                           Write(Strings.Common_RequeueJobs);

            
            #line default
            #line hidden
WriteLiteral("\r\n                            </button>\r\n\r\n                            ");


            
            #line 117 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                       Write(Html.PerPageSelector(pager));

            
            #line default
            #line hidden
WriteLiteral(@"
                        </div>

                        <div class=""table-responsive"">
                            <table class=""table"">
                                <thead>
                                    <tr>
                                        <th class=""min-width"">
                                            <input type=""checkbox"" class=""js-jobs-list-select-all"" />
                                        </th>
                                        <th class=""min-width"">");


            
            #line 127 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                         Write(Strings.Common_Id);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n                                        <th>");


            
            #line 128 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                       Write(Strings.Common_Job);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n                                        <th class=\"min-width\">");


            
            #line 129 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                         Write(Strings.Common_State);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n                                        <th class=\"align-right\">");


            
            #line 130 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                           Write(Strings.RecurringJobsPage_Table_LastExecution);

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n                                    </tr>\r\n                               " +
" </thead>\r\n                                <tbody>\r\n");


            
            #line 134 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                     foreach (var job in matchingJobs)
                                    {

            
            #line default
            #line hidden
WriteLiteral("                                        <tr class=\"js-jobs-list-row\">\r\n          " +
"                                  <td>\r\n                                        " +
"        <input type=\"checkbox\" class=\"js-jobs-list-checkbox\" name=\"jobs[]\" value" +
"=\"");


            
            #line 138 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                                                                                     Write(job.Key);

            
            #line default
            #line hidden
WriteLiteral("\" />\r\n                                            </td>\r\n                        " +
"                    <td class=\"min-width\">\r\n                                    " +
"            ");


            
            #line 141 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                           Write(Html.JobIdLink(job.Key));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                            </td>\r\n\r\n");


            
            #line 144 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                             if (job.Value == null)
                                            {

            
            #line default
            #line hidden
WriteLiteral("                                                <td colspan=\"3\">\r\n               " +
"                                     <em>");


            
            #line 147 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                   Write(Strings.Common_JobExpired);

            
            #line default
            #line hidden
WriteLiteral("</em>\r\n                                                </td>\r\n");


            
            #line 149 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                            }
                                            else
                                            {

            
            #line default
            #line hidden
WriteLiteral("                                                <td class=\"word-break\">\r\n        " +
"                                            ");


            
            #line 153 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                               Write(Html.JobNameLink(job.Key, job.Value.Job));

            
            #line default
            #line hidden
WriteLiteral("\r\n                                                </td>\r\n");



WriteLiteral("                                                <td class=\"min-width\">\r\n         " +
"                                           ");


            
            #line 156 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                               Write(job.Value.State);

            
            #line default
            #line hidden
WriteLiteral("\r\n                                                </td>\r\n");



WriteLiteral("                                                <td class=\"min-width align-right\"" +
">\r\n");


            
            #line 159 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                     if (job.Value.ResultAt.HasValue)
                                                    {
                                                        
            
            #line default
            #line hidden
            
            #line 161 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                   Write(Html.RelativeTime(job.Value.ResultAt.Value));

            
            #line default
            #line hidden
            
            #line 161 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                                                                                    
                                                    }

            
            #line default
            #line hidden
WriteLiteral("                                                </td>\r\n");


            
            #line 164 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                            }

            
            #line default
            #line hidden
WriteLiteral("                                        </tr>\r\n");


            
            #line 166 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                                    }

            
            #line default
            #line hidden
WriteLiteral("                                </tbody>\r\n                            </table>\r\n " +
"                       </div>\r\n\r\n                        ");


            
            #line 171 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                   Write(Html.Paginator(pager));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n");


            
            #line 173 "..\..\Dashboard\Pages\TagsSearchPage.cshtml"
                }
            }
        

            
            #line default
            #line hidden
WriteLiteral("    </div>\r\n</div>\r\n");


        }
    }
}
#pragma warning restore 1591
