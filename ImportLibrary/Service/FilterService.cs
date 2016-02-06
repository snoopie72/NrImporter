using System.Collections.Generic;
using System.Linq;
using Northernrunners.ImportLibrary.Dto;
using Northernrunners.ImportLibrary.Poco;
using Northernrunners.ImportLibrary.Service.Datalayer;

namespace Northernrunners.ImportLibrary.Service
{
    public class FilterService:IFilterService
    {
        private readonly IDatalayerService _datalayerService;

        public FilterService(IDatalayerService datalayerService)
        {
            _datalayerService = datalayerService;
        }


        public ICollection<Filter> GetFilters()
        {
            var filters = _datalayerService.GetFilters();
            return filters.Select(filter => new Filter
            {
                Value = filter.FilterValue, Type = filter.FilterKey.Equals(FilterType.Equals.ToString()) ? FilterType.Equals : FilterType.Contains
            }).ToList();
        }

        public void SaveFilters(ICollection<Filter> filters)
        {
            var filterDtoList = new List<FilterDto>();
            var i = 0;
            foreach (var filter in filters)
            {
                i++;
                var filterDto = new FilterDto
                {
                    Id = i,
                    FilterValue = filter.Value,
                    FilterKey = filter.Type.ToString()
                };
                filterDtoList.Add(filterDto);
            }
            _datalayerService.SaveFilters(filterDtoList);
        }
    }
}
