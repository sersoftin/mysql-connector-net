﻿// Copyright © 2016, Oracle and/or its affiliates. All rights reserved.
//
// MySQL Connector/NET is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most 
// MySQL Connectors. There are special exceptions to the terms and 
// conditions of the GPLv2 as it is applied to this software, see the 
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify 
// it under the terms of the GNU General Public License as published 
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License 
// for more details.
//
// You should have received a copy of the GNU General Public License along 
// with this program; if not, write to the Free Software Foundation, Inc., 
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
using MySQL.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace MySql.Data.EntityFrameworkCore.Query.Internal
{
    public class MySQLQueryCompilationContextFactory : RelationalQueryCompilationContextFactory
    {
        public MySQLQueryCompilationContextFactory(
            [NotNull] IModel model,
            [NotNull] ISensitiveDataLogger<MySQLQueryCompilationContextFactory> logger, 
            [NotNullAttribute] IEntityQueryModelVisitorFactory entityQueryModelVisitorFactory,
            [NotNullAttribute] IRequiresMaterializationExpressionVisitorFactory requiresMaterializationExpressionVisitorFactory,
            [NotNullAttribute] MethodInfoBasedNodeTypeRegistry methodInfoBasedNodeTypeRegistry,
            [NotNullAttribute] ICurrentDbContext currentContext) : 
            base(model, logger, entityQueryModelVisitorFactory, requiresMaterializationExpressionVisitorFactory, methodInfoBasedNodeTypeRegistry, currentContext)
        {
        }

        public override QueryCompilationContext Create(bool async)
        {
            if (!async)
            { 
             return new MySQLQueryCompilationContext(
                    Model,
                    (ISensitiveDataLogger)Logger,
                    EntityQueryModelVisitorFactory,
                    RequiresMaterializationExpressionVisitorFactory,
                    new LinqOperatorProvider(),
                    new QueryMethodProvider(),
                    ContextType,
                    TrackQueryResults);

            }
            else
                return base.Create(async);
        }
    }
}