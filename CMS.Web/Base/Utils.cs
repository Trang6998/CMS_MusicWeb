using System;
using System.Text.RegularExpressions;

namespace CMS.Web.Base
{
    public sealed class Utils
    {
        public static string Pagination(string url, 
            int total, 
            int page = 1, 
            int pageSize = 10, 
            int offset = 5, 
            string otherParams = null)
        {
            if (total > 0)
            {
                double rowPerPage = pageSize;
                if (total < pageSize)
                {
                    rowPerPage = total;
                }

                int totalPage = Convert.ToInt16(Math.Ceiling(total / rowPerPage));
                int current = page;
                int record = offset;
                int pageStart = current - offset;
                int pageEnd = current + offset;
                string numPage = "";
                if (totalPage < 1)
                { return ""; }

                int itemFrom = (int)(page * rowPerPage - rowPerPage + 1);

                int itemTo = (int)(page * rowPerPage);
                if (itemTo > total)
                {
                    itemTo = total;
                }

                numPage += "<div class='pagination-area my-2'><div class='row align-items-center'><div class='col-lg-6 col-md-6'><ul class='pagination'>";
                if (current > 1)
                {
                    numPage += $"<li class='page-item'><a class='page-link' href='{url}?page=" + (page - 1) + otherParams + "' aria-label='Previous'><i class='fa fa-angle-left'></i></a></li>";
                }
                else
                {
                    numPage += "<li class='page-item disabled'><a class='page-link' href='#' aria-label='Previous'><i class='fa fa-angle-left'></i></a></li>";
                }
                if (current > (offset + 1))
                {
                    numPage += "<li class='page-item'><a class='page-link' href='" + url + "?page=1" + otherParams + "' name='page1'>1</a></li><li class='page-item disabled spacing-dot'><span class='page-link'>...</a></li>";
                }
                for (int i = 1; i <= totalPage; i++)
                {
                    if (pageStart <= i && pageEnd >= i)
                    {
                        if (i == current)
                        {
                            numPage += "<li class='page-item active'><span class='page-link'>" + i + "</span></li>";
                        }
                        else
                        {
                            numPage += "<li class='page-item'><a class='page-link' href='" + url + "?page=" + i + otherParams + "'>" + i + "</a></li>";
                        }
                    }
                }
                if (totalPage > pageEnd)
                {
                    record = offset;
                    numPage += "<li class='page-item disabled spacing-dot'><span class='page-link'>...</span></li><li><a class='page-link' href='" + url + "?page=" + (totalPage) + otherParams + "'>" + totalPage + "</a></li>";
                }
                if (current < totalPage)
                {
                    numPage += "<li class='page-item next'><a class='page-link' class='ui-bar-d' href='" + url + "?page=" + (page + 1) + otherParams + "'><i class='fa fa-angle-right'></i></a></li>";
                }
                else
                {
                    numPage += "<li class='page-item'><span class='page-link' aria-label='Previous'><i class='fa fa-angle-double-right'></i></span></li>";
                }

                numPage += "</div><div class='col-lg-6 col-md-6'><div class='page-amount d-flex'>";
                numPage += $"<p>Hiển thị {itemFrom} đến {itemTo} trên {total}</p>";
                numPage += "</div></div></div></div>";
                return numPage;
            }
            else
            {
                return "<p class=\"mt-4\">Không có dữ liệu.<p>";
            }
        }
        public static String Filter(String userInput)
        {
            Regex re = new Regex("([^A-Za-z0-9@.' _-]+)");
            String filtered = re.Replace(userInput, "_");
            return filtered;
        }
    }
}