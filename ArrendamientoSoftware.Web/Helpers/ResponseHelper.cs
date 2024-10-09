﻿using ArrendamientoSoftware.Web.Core;

namespace ArrendamientoSoftware.Web.Helpers
{
    public static class ResponseHelper<T>
    {

        public static Response<T> MakeResponseSucess(T model, string message = "Tarea realizada con éxito")
        {
            return new Response<T>
            {
                IsSucess = true,
                Message = message,
                Result = model,
            };
        }

        public static Response<T> MakeResponseFail(Exception ex, string message = "Error al generar la solicitud")
        {
            return new Response<T>
            {
                Errors = new List<string>
                {
                    ex.Message
                },

                IsSucess = false,
                Message = message,
            };
        }

        public static Response<T> MakeResponseFail(string message)
        {
            return new Response<T>
            {
                Errors = new List<string>
                {
                    message
                },

                IsSucess = false,
                Message = message,
            };
        }
    }
}
