﻿using MasteringEFCore.Concurrencies.Final.Core.Commands.Files;
using MasteringEFCore.Concurrencies.Final.Data;
using MasteringEFCore.Concurrencies.Final.Helpers;
using MasteringEFCore.Concurrencies.Final.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Final.Infrastructure.Commands.Files
{
    public class DeleteFileCommand : CommandFileBase, ICreateFileCommand<int>
    {
        public DeleteFileCommand(BlogFilesContext context) : base(context)
        {
        }

        public Guid Id { get; set; }

        public int Handle()
        {
            int returnValue = 0;
            try
            {
                DeleteFile();
                returnValue = Context.SaveChanges();
            }
            catch (Exception exception)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            }

            return returnValue;
        }

        public async Task<int> HandleAsync()
        {
            int returnValue = 0;
            try
            {
                DeleteFile();
                returnValue = await Context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            }

            return returnValue;
        }

        private void DeleteFile()
        {
            var post = Context.Files.SingleOrDefault(m => m.Id == Id);
            Context.Files.Remove(post);
        }
    }
}
