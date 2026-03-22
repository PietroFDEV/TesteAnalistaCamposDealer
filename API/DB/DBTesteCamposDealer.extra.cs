using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace TesteCamposDealer.DB
{
    public partial class DBTesteCamposDealerDataContext
    {
        public Table<VendaItem> VendaItem
        {
            get { return this.GetTable<VendaItem>(); }
        }

        public Table<ProdutoPrecoHistorico> ProdutoPrecoHistorico
        {
            get { return this.GetTable<ProdutoPrecoHistorico>(); }
        }
    }

    [Table(Name = "dbo.VendaItem")]
    public partial class VendaItem
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY")]
        public int Id { get; set; }

        [Column(DbType = "Int NOT NULL")]
        public int IdVenda { get; set; }

        [Column(DbType = "Int NOT NULL")]
        public int IdProduto { get; set; }

        [Column(DbType = "Int NOT NULL")]
        public int Quantidade { get; set; }

        [Column(DbType = "Decimal(18,2) NOT NULL")]
        public decimal ValorUnitario { get; set; }

        public decimal ValorTotal
        {
            get { return Quantidade * ValorUnitario; }
        }
    }

    [Table(Name = "dbo.ProdutoPrecoHistorico")]
    public partial class ProdutoPrecoHistorico
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY")]
        public int Id { get; set; }

        [Column(DbType = "Int NOT NULL")]
        public int ProdutoId { get; set; }

        [Column(DbType = "Decimal(18,2) NOT NULL")]
        public decimal Preco { get; set; }

        [Column(DbType = "DateTime NOT NULL")]
        public DateTime DataAlteracao { get; set; }
    }

    public partial class Produto
    {
        private decimal _vlrProduto;

        [Column(Storage = "_vlrProduto", DbType = "Decimal(18,2) NOT NULL")]
        public decimal vlrProduto
        {
            get { return _vlrProduto; }
            set
            {
                if (_vlrProduto != value)
                {
                    SendPropertyChanging();
                    _vlrProduto = value;
                    SendPropertyChanged("vlrProduto");
                }
            }
        }
    }

    public partial class Venda
    {
        private decimal _vlrTotal;

        [Column(Storage = "_vlrTotal", DbType = "Decimal(18,2) NOT NULL")]
        public decimal vlrTotal
        {
            get { return _vlrTotal; }
            set
            {
                if (_vlrTotal != value)
                {
                    SendPropertyChanging();
                    _vlrTotal = value;
                    SendPropertyChanged("vlrTotal");
                }
            }
        }

        public DateTime dtVenda
        {
            get { return dthRegistro; }
            set { dthRegistro = value; }
        }
    }
}
