#!/bin/bash
set -a
source .env
set +a

dotnet ef dbcontext scaffold "Server=localhost;Database=testdb;User Id=testuser;Password=testpass;"   Npgsql.EntityFrameworkCore.PostgreSQL  --output-dir ../Core.Domain/Entities   --context-dir .   --context MyDbContext --no-onconfiguring  --namespace Core.Domain.Entities --context-namespace  Infrastructure.Postgres.Scaffolding --force 

