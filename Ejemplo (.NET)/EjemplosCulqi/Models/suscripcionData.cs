using System;
using System.Collections.Generic;
using System.Globalization;

namespace EjemplosCulqi.Models
{
    public class suscripcionData
    {
        private string codigo_comercio;
        private string codigo_pais;
        private string direccion;
        private string ciudad;
        private string telefono;
        private string nombre;
        private string apellido;
        private string correo_electronico;
        private string usuarioId;
        private string plan_id;
        private string token;

        public string Codigo_comercio
        {
            get { return codigo_comercio; }
            set { codigo_comercio = value; }
        }
        public string Codigo_pais
        {
            get { return codigo_pais; }
            set { codigo_pais = value; }
        }
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        public string Correo_electronico
        {
            get { return correo_electronico; }
            set { correo_electronico = value; }
        }
        public string UsuarioId
        {
            get { return usuarioId; }
            set { usuarioId = value; }
        }
        public string Plan_id
        {
            get { return plan_id; }
            set { plan_id = value; }
        }
        public string Token
        {
            get { return token; }
            set { token = value; }
        }
    }
}