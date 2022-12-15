using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using SiteOfMe.Models;

namespace SiteOfMe.DAL
{    
    public class UnitOfWork : IDisposable
    {
        private SiteOfMeDbContext _siteOfMeDbContext;
        private PostRepository _postRep;
        private UserRepository _userRep;
        private RoleRepository _roleRep;
        private CommentRepository _commentRep;
        private TagRepository _tagRep;
        private DownFileRepository _downFileRep;
        private SessionRepository _sessionRep;
        private PostRelateRepository _postRelateRep;
        private QuoteRepository _quoteRep;
        private AnonymousUserRepository _anonymousUserRep;
        private AppendixRepository _appendixRep;
        private RelatedLinkRepository _relatedLinkRep;
        private QuoterRepository _quoterRep;

        private bool _isDisposed = false;

        public UnitOfWork(SiteOfMeDbContext siteOfMeDbContext=null)
        {
            _siteOfMeDbContext = siteOfMeDbContext ?? new SiteOfMeDbContext();
        }

        public PostRepository PostRep
        {
            get { return _postRep ?? (_postRep = new PostRepository(_siteOfMeDbContext)); }
        }

        public AppendixRepository AppendixRep
        {
            get { return _appendixRep ?? (_appendixRep = new AppendixRepository(_siteOfMeDbContext)); }
        }

        public RelatedLinkRepository RelatedLinkRep
        {
            get { return _relatedLinkRep ?? (_relatedLinkRep = new RelatedLinkRepository(_siteOfMeDbContext)); }
        }

        public AnonymousUserRepository AnonymousUserRep
        {
            get { return _anonymousUserRep ?? (_anonymousUserRep = new AnonymousUserRepository(_siteOfMeDbContext)); }
        }
        public UserRepository UserRep
        {
            get { return _userRep ?? (_userRep = new UserRepository(_siteOfMeDbContext)); }
        }
        public RoleRepository RoleRep
        {
            get { return _roleRep ?? (_roleRep = new RoleRepository(_siteOfMeDbContext)); }
        }
        public CommentRepository CommentRep
        {
            get { return _commentRep ?? (_commentRep = new CommentRepository(_siteOfMeDbContext)); }
        }
        public TagRepository TagRep
        {
            get { return _tagRep ?? (_tagRep = new TagRepository(_siteOfMeDbContext)); }
        }
        public DownFileRepository DownFileRep
        {
            get { return _downFileRep ?? (_downFileRep = new DownFileRepository(_siteOfMeDbContext)); }
        }
        public SessionRepository SessionRep
        {
            get { return _sessionRep ?? (_sessionRep = new SessionRepository(_siteOfMeDbContext)); }
        }
        public PostRelateRepository PostRelateRep
        {
            get { return _postRelateRep ?? (_postRelateRep = new PostRelateRepository(_siteOfMeDbContext)); }
        }
        public QuoteRepository QuoteRep
        {
            get { return _quoteRep ?? (_quoteRep = new QuoteRepository(_siteOfMeDbContext)); }
        }

        public QuoterRepository QuoterRep
        {
            get { return _quoterRep ?? (_quoterRep = new QuoterRepository(_siteOfMeDbContext)); }
        }

        public void SaveChanges()
        {
            try
            {
                _siteOfMeDbContext.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                string failMsg = string.Empty;
                foreach (var dbEntityValidationResult in exception.EntityValidationErrors)
                {
                    foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        failMsg += string.Format("Property: {0}\tError: {1}\r\n", 
                            dbValidationError.PropertyName, dbValidationError.ErrorMessage);
                    }
                }
                throw new Exception(failMsg);
            }
        }

        public void Dispose()
        {
            if(!_isDisposed)
            {
                _siteOfMeDbContext.Dispose();
                _isDisposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}