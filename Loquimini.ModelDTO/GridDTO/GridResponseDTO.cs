using System.Collections.Generic;

namespace Loquimini.ModelDTO.GridDTO
{
    public class GridResponseDTO<TDto>
    {
        public int Total { get; set; }

        public IEnumerable<TDto> Data { get; set; }
    }
}
