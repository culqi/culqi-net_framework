using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjemplosCulqi.Models
{
    public class VentaData
    {
        private string token;
        private string moneda;
        private string monto;
        private string descripcion;
        private string pedido;
        private string codigo_pais;
        private string ciudad;
        private string usuario;
        private string direccion;
        private string telefono;
        private string nombres;
        private string apellidos;
        private string correo_electronico;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        public string Moneda
        {
            get { return moneda; }
            set { moneda = value; }
        }
        public string Monto
        {
            get { return monto; }
            set { monto = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        public string Pedido
        {
            get { return pedido; }
            set { pedido = value; }
        }
        public string Codigo_pais
        {
            get { return codigo_pais; }
            set { codigo_pais = value; }
        }
        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }
        public string Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        public string Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }
        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }
        public string Correo_electronico
        {
            get { return correo_electronico; }
            set { correo_electronico = value; }
        }
    }
}