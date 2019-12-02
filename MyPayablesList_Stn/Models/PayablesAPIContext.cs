using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace MyPayablesList_Stn.Models
{
    public partial class PayablesAPIContext : DbContext
    {
        public PayablesAPIContext()
        {
        }

        public PayablesAPIContext(DbContextOptions<PayablesAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CadOrganizacao> CadOrganizacaoItens { get; set; }
        public virtual DbSet<CadPessoa> CadPessoaItens { get; set; }
        public virtual DbSet<FinLancamento> FinLancamentoItens { get; set; }
        public virtual DbSet<FinLancamentoQueryable> FinLancamentoItensRetorno { get; set; }


        public IConfiguration Configuration { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Configuration["Data:APIConnection:PgConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<CadOrganizacao>(entity =>
            {
                entity.HasKey(e => e.OrgOrganizacaoId)
                    .HasName("cad_org_organizacao_pkey");

                entity.ToTable("cad_organizacao");

                entity.HasIndex(e => e.OrgNome)
                    .HasName("cad_org_nome_org_nome_key")
                    .IsUnique();

                entity.Property(e => e.OrgOrganizacaoId)
                    .HasColumnName("org_organizacao_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.OrgCategoria)
                    .HasColumnName("org_categoria")
                    .HasMaxLength(35)
                    .HasDefaultValueSql("'Variados'::character varying");

                entity.Property(e => e.OrgNome)
                    .IsRequired()
                    .HasColumnName("org_nome")
                    .HasMaxLength(65);
            });

            modelBuilder.Entity<CadPessoa>(entity =>
            {
                entity.HasKey(e => e.PesPessoaId)
                    .HasName("pes_pessoa_id_pkey");

                entity.ToTable("cad_pessoa");

                entity.Property(e => e.PesPessoaId)
                    .HasColumnName("pes_pessoa_id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.PesNome)
                    .HasColumnName("pes_nome")
                    .HasMaxLength(65);

                entity.Property(e => e.PesSenha)
                    .HasColumnName("pes_senha")
                    .HasMaxLength(20);

                entity.Property(e => e.PesUsuario)
                    .HasColumnName("pes_usuario")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<FinLancamento>(entity =>
            {
                entity.HasKey(e => e.LanLancamentoId)
                    .HasName("fin_lancamento_pkey");

                entity.ToTable("fin_lancamento");

                entity.Property(e => e.LanLancamentoId).HasColumnName("lan_lancamento_id");

                entity.Property(e => e.LanDataLancamento)
                    .HasColumnName("lan_data_lancamento")
                    .HasDefaultValueSql("('now'::text)::date");

                entity.Property(e => e.LanFormaPagamento)
                    .IsRequired()
                    .HasColumnName("lan_forma_pagamento")
                    .HasMaxLength(15);

                entity.Property(e => e.LanMoeda)
                    .HasColumnName("lan_moeda")
                    .HasMaxLength(3)
                    .IsFixedLength()
                    .HasDefaultValueSql("'BRL'::character(3)");

                entity.Property(e => e.LanOrgOrganizacaoId).HasColumnName("lan_org_organizacao_id");

                entity.Property(e => e.LanPesPessoaId).HasColumnName("lan_pes_pessoa_id");

                entity.Property(e => e.LanValorLancamento)
                    .HasColumnName("lan_valor_lancamento")
                    .HasColumnType("numeric(12,4)")
                    .HasDefaultValueSql("'0'::numeric");
            });

            modelBuilder.Entity<FinLancamentoQueryable>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LanDataLancamento)
                    .HasColumnName("lan_data_lancamento")
                    .HasDefaultValueSql("('now'::text)::date");

                entity.Property(e => e.OrgCategoria)
                    .HasColumnName("org_categoria")
                    .HasMaxLength(35)
                    .HasDefaultValueSql("'Variados'::character varying");

                entity.Property(e => e.LanFormaPagamento)
                    .IsRequired()
                    .HasColumnName("lan_forma_pagamento")
                    .HasMaxLength(15);

                entity.Property(e => e.LanMoeda)
                    .HasColumnName("lan_moeda")
                    .HasMaxLength(3)
                    .IsFixedLength()
                    .HasDefaultValueSql("'BRL'::character(3)");

                entity.Property(e => e.LanValorTotal)
                    .HasColumnName("lan_valor_total")
                    .HasColumnType("numeric(12,4)")
                    .HasDefaultValueSql("'0'::numeric");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
