using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Koleo.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Users
{
    public class List
    {
        public class Query : IRequest<List<User>> { }
        public class Handler : IRequestHandler<Query, List<User>>
        {
            private readonly DataContext dataContext;
            public Handler(DataContext dataContext)
            {
                this.dataContext = dataContext;
            }
            public async Task<List<User>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await dataContext.Users.ToListAsync();
            }
        }
    }
}