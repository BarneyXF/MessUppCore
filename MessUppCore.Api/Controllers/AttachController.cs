using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MessUppCore.DataLayer;
using MessUppCore.DataLayer.Sql;
using MessUppCore.Model;
using Microsoft.AspNetCore.Mvc;

namespace MessUppCore.Api.Controllers
{
    /// <summary>
    ///     Реализация контроллера для вложений.
    /// </summary>
    [SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class AttachController : Controller
    {
        private readonly IAttachmentRepository _attachRepository;

        /// <summary>
        ///     Конструктор методов работы с вложениями.
        /// </summary>
        [SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
        public AttachController()
        {
            _attachRepository = new AttachmentRepository(Constants.Constants.ConnectionString);
        }

        /// <summary>
        ///     Запрос на создание вложения.
        /// </summary>
        /// <param name="attach">Данные вложения.</param>
        /// <returns>Данные вложения с идентификатором.</returns>
        /// <exception cref="System.Web.Http.HttpResponseException">Ошибка обработки запроса.</exception>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public Attachment CreateAttachment([Microsoft.AspNetCore.Mvc.FromBody] Attachment attach)
        {
            try
            {
                return _attachRepository.LoadAttachment(attach);
            }
            catch (SqlException exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(exception.Message)
                };
                throw new HttpResponseException(response);
            }
        }

        /// <summary>
        ///     Запрос на получение вложения по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор вложения.</param>
        /// <returns>Данные вложения.</returns>
        /// <exception cref="System.Web.Http.HttpResponseException">Ошибка обработки запроса.</exception>
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public Attachment GetAttachment(Guid id)
        {
            try
            {
                return _attachRepository.GetAttachment(id);
            }
            catch (SqlException exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(exception.Message)
                };
                throw new HttpResponseException(response);
            }
            catch (Exception exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(exception.Message)
                };
                throw new HttpResponseException(response);
            }
        }

        /// <summary>
        ///     Запрос на удаление вложения по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор вложения.</param>
        /// <exception cref="System.Web.Http.HttpResponseException">Ошибка обработки запроса.</exception>
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public void DeleteChat(Guid id)
        {
            try
            {
                _attachRepository.DeleteAttachment(id);
            }
            catch (SqlException exception)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(exception.Message)
                };
                throw new HttpResponseException(response);
            }
        }
    }
}