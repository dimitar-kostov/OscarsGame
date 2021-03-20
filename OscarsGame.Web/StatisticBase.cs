using System.Data;
using System.Web.UI.WebControls;

namespace OscarsGame
{
    public class StatisticBase : BasePage
    {
        protected void SetSortingArrows(GridView gridView, SortDirection gridViewSortDirection, string sortingExpression)
        {
            if (gridViewSortDirection == SortDirection.Ascending)
            {
                gridView.HeaderRow.Cells[GetColumnIndex(sortingExpression, gridView)].CssClass = "sortasc";
            }
            else
            {
                gridView.HeaderRow.Cells[GetColumnIndex(sortingExpression, gridView)].CssClass = "sortdesc";
            }
        }

        protected int GetColumnIndex(string SortExpression, GridView gridViewToSearch)
        {
            int columnIndex = 0;

            foreach (DataControlField c in gridViewToSearch.Columns)
            {
                if (c.SortExpression == SortExpression)
                    break;

                columnIndex++;
            }

            return columnIndex;
        }

        protected DataView GetDefaultTableSort(DataTable dataTable, string sortExpresion, SortDirection gridViewSortDirection)
        {
            DataView dataView = new DataView(dataTable);

            if (gridViewSortDirection == SortDirection.Ascending)
            {
                dataView.Sort = sortExpresion + " ASC";
            }
            else
            {
                dataView.Sort = sortExpresion + " DESC";
            }

            return dataView;
        }
    }
}