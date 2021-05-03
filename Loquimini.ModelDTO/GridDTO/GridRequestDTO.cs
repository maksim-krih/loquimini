using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Loquimini.ModelDTO.GridDTO
{
    public class GridRequestDTO
    {
        public List<GridFilterDTO> Filters { get; set; }

        public GridPagerDTO Pager { get; set; }

        public List<GridSorterDTO> Sorter { get; set; }

        public GridSearchDTO Search { get; set; }

        public GridResponseDTO<TData> GenerateGridResponse<TData>(IQueryable<TData> collection)
        {
            ApplyFiltering(ref collection);
            ApplySorting(ref collection);
            ApplySearching(ref collection);

            var total = collection.Count();
            collection = collection.Skip(Pager.PageSize * (Pager.Current - 1)).Take(Pager.PageSize);

            return new GridResponseDTO<TData> { Data = collection.ToList(), Total = total };
        }

        public async Task<GridResponseDTO<TData>> GenerateGridResponseAsync<TData>(IQueryable<TData> collection)
        {
            ApplyFiltering(ref collection);
            ApplySorting(ref collection);
            ApplySearching(ref collection);

            var total = await collection.CountAsync();
            collection = collection.Skip(Pager.PageSize * (Pager.Current - 1)).Take(Pager.PageSize);

            return new GridResponseDTO<TData> { Data = await collection.ToListAsync(), Total = total };
        }

        public void ApplySearching<TEntity>(ref IQueryable<TEntity> collection)
        {

            if (Search != null && Search.Fields?.Count() > 0)
            {
                string whereClause = string.Empty;
                string[] arr = Search.Fields.ToArray();
                var parameters = new List<object>();

                for (var i = 0; i < Search.Fields.Count(); i++)
                {
                    if (i == 0)
                            whereClause +=
                                $" {DbCommandConverter.BuildWhereClause<TEntity>(i, new GridFilterDTO { Field = arr[i], Value = Search?.Value?.ToLower(), Operator = "contains" }, parameters)}";
                        else
                            whereClause +=
                                $" {DbCommandConverter.ToLinqOperator("or")} {DbCommandConverter.BuildWhereClause<TEntity>(i, new GridFilterDTO { Field = arr[i], Value = Search?.Value?.ToLower(), Operator = "contains" }, parameters)}";
                    }
                if (whereClause != null)
                {
                    collection = collection.Where(whereClause, parameters.ToArray());
                }
            }

        }

        public void ApplySorting<TEntity>(ref IQueryable<TEntity> collection)
        {

            if (Sorter != null && Sorter.Count() > 0)
                collection = Sorter.Aggregate(collection,
                    (current, sortItem) => current.OrderBy($"{sortItem.Field} {sortItem.Order}"));
        }

        public void ApplyFiltering<TEntity>(ref IQueryable<TEntity> collection)
        {
            if (Filters != null)
            {
                string whereClause = null;
                var parameters = new List<object>();
                var appliedFilters = Filters.Where(f => f.Value != null).ToList();

                for (var i = 0; i < appliedFilters.Count(); i++)
                {
                    var whereCondition = DbCommandConverter.BuildWhereClause<TEntity>(i, appliedFilters[i], parameters);
                    if (whereCondition != null)
                    {
                        whereClause += $" {whereCondition} ";

                        if (i != Filters.Count() - 1)
                        {
                            whereClause += appliedFilters[i].Logic;
                        }
                    }
                }
                if (whereClause != null)
                {
                    collection = collection.Where(whereClause, parameters.ToArray());
                }
            }
        }
    }
}
