using Mapster;

public class IndirizzoCittaMappers : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<IndirizzoEntity, NomeCittaDto>()
            .Map(dest => dest.LinkCittaNome, src => src.LinkCitta.Nome);

        config.NewConfig<IndirizzoEntity, IndirizzoDto>()

            .Map(dest => dest.LinkCittaNome, src => src.LinkCitta.Nome)


            ;

        config.NewConfig<IndirizzoDto, IndirizzoEntity>()
            .AfterMapping((src, dst) =>
            {
                var repository = MapContext.Current.GetService<IEntityBaseRepository<CittaEntity>>();
                var cittaEsiste = repository.All.FirstOrDefault(x => x.Nome == src.LinkCittaNome);
                if (cittaEsiste != null)
                {
                    dst.LinkCittaId = cittaEsiste.Id;
                    dst.LinkCitta = cittaEsiste;
                }
            })
            .Map(x=>x.LinkCitta.Nome, dst=> dst.LinkCittaNome)
            .Map(x=>x.LinkCitta.Cap, dst=> "---");

        // config.NewConfig<RequestPersonaDTO, PersonaEntity>()
        //     // .Map(dest=> dest.Cognome, src=>src.cognome)
        // ;
        config.NewConfig<PersonaEntity, RequestPersonaDTO>()
            .TwoWays()
            ;

        config.NewConfig<PersonaEntity, RubricaDto>()
            
            // .AfterMapping((src, dst) =>
            // {
            //     if (src.ListIndirizzi.Any())
            //     {
            //         dst.Message = "Ha indirizzi";
            //         Console.WriteLine("LA persona presenta degli indirizzi");
            //     }else
            //     {
            //         dst.Message = "Non ha indirizzi";
            //     } 

            // })
            
            //.Map(dest =>dest.Message, src => src.ListIndirizzi.Any() ? "Ha Qualche indirizzo" : "Non ha indirizzi")
            //.IgnoreNullValues(true)
            ;
    }
}