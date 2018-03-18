﻿using System;
using System.Threading.Tasks;
using Test.DAL.Abstract;
using Test.MODELS;

namespace Test.DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }


        private IQuestionsRepository _questionsRepository;

        public IQuestionsRepository QuestionsRepository
        {
            get
            {
                if (_questionsRepository == null)
                {
                    _questionsRepository = new QuestionsRepository(_context);
                }
                return _questionsRepository;
            }
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        // IDisposable
        readonly bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
