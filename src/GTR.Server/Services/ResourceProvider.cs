#region

using GTR.Core.Services;

#endregion

namespace GTR.Server.Services
{
    public class ResourceProvider : IResourceProvider
    {
        public string CardXml { get; } = @"<?xml version=""1.0"" encoding=""utf-8""?>

<cards xmlns = ""http://tempuri.org/CardsSchema.xsd""
       xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
       xsi:schemaLocation=""http://tempuri.org/ CardsSchema.xsd"">
 <!--Brick-->
  <card>
    <building>Academy</building>
    <material>brick</material>
    <description>You may execute one Thinker action after any turn in which you execute at least one Craftsman action.</description>
  </card>
  <card>
    <building>Atrium</building>
    <material>brick</material>
    <description>When performing MERCHANT action may take from DECK (do not look at card).</description>
  </card>
  <card>
    <building>Bath</building>
    <material>brick</material>
    <description>When performing PATRON action each client you hire may perform its action once as it enters CLIENTELE.</description>
  </card>
  <card>
    <building>Foundry</building>
    <material>brick</material>
    <description>May perform one LABORER action for each INFLUENCE.</description>
  </card>
  <card>
    <building>Gate</building>
    <material>brick</material>
    <description>Incomplete MARBLE structures provide function immediately</description>
  </card>
  <card>
    <building>School</building>
    <material>brick</material>
    <description>May perform one THINKER action for each INFLUENCE</description>
  </card>
  <card>
    <building>Shrine</building>
    <material>brick</material>
    <description>Maximum HAND + 2</description>
  </card>
  <!--Concrete-->
  <card>
    <building>Amphitheatre</building>
    <material>concrete</material>
    <description>May perform one CRAFTSMAN action for each INFLUENCE</description>
  </card>
  <card>
    <building>Aqueduct</building>
    <material>concrete</material>
    <description>Your maximum Clientele is doubled. Every time you perform the Patron action you may choose to take an additional card from your hand as a Client.</description>
  </card>
  <card>
    <building>Bridge</building>
    <material>concrete</material>
    <description>When performing LEGIONARY action may take material from STOCKPILE.  Ignore Palisades.  May take from all opponents</description>
  </card>
  <card>
    <building>Senate</building>
    <material>concrete</material>
    <description>May take opponent's JACK into HAND at end of turn in which it is played</description>
  </card>
  <card>
    <building>Storeroom</building>
    <material>concrete</material>
    <description>All clients count as LABORERS</description>
  </card>
  <card>
    <building>Tower</building>
    <material>concrete</material>
    <description>May use RUBBLE in any STRUCTURE.  May lay foundation onto any out of town SITE at no extra cost</description>
  </card>
  <card>
    <building>Vomitorium</building>
    <material>concrete</material>
    <description>Before performing THINKER action may discard all cards to POOL</description>
  </card>
  <card>
    <building>Wall</building>
    <material>concrete</material>
    <description>Immune to LEGIONARY.  + 1 VP for every two materials in STOCKPILE</description>
  </card>
  <!--Marble-->
  <card>
    <building>Basilica</building>
    <material>marble</material>
    <description>Each time you execute a Merchant action you may choose to take an additional card from your hand as a Material into your Vault.</description>
  </card>
  <card>
    <building>Forum</building>
    <material>marble</material>
    <description>One client of each role wins game</description>
  </card>
  <card>
    <building>Fountain</building>
    <material>marble</material>
    <description>When performing CRAFTSMAN action may use cards from DECK.  Retain any unused cards in HAND</description>
  </card>
  <card>
    <building>Ludus Magnus</building>
    <material>marble</material>
    <description>Each MERCHANT client counts as any role</description>
  </card>
  <card>
    <building>Palace</building>
    <material>marble</material>
    <description>May play multiple cards of same role in order to perform additional actions</description>
  </card>
  <card>
    <building>Stairway</building>
    <material>marble</material>
    <description>When performing ARCHITECT action may add material to opponent's completed STRUCTURE to make function available to all players</description>
  </card>
  <card>
    <building>Statue</building>
    <material>marble</material>
    <description>+ 3 VP. May place Statue on any SITE</description>
  </card>
  <card>
    <building>Temple</building>
    <material>marble</material>
    <description>Maximum HAND + 4</description>
  </card>
  <!--Rubble-->
  <card>
    <building>Bar</building>
    <material>rubble</material>
    <description>When performing PATRON action may take card from DECK</description>
  </card>
  <card>
    <building>Insula</building>
    <material>rubble</material>
    <description>Maximum CLIENTELE + 2</description>
  </card>
  <card>
    <building>Latrine</building>
    <material>rubble</material>
    <description>Before performing THINKER action may discard one card to POOL</description>
  </card>
  <card>
    <building>Road</building>
    <material>rubble</material>
    <description>When adding to STONE structure may use any material</description>
  </card>
  <!--Stone-->
  <card>
    <building>Catacomb</building>
    <material>stone</material>
    <description>Game ends immediately.  Score as usual</description>
  </card>
  <card>
    <building>Circus Maximus</building>
    <material>stone</material>
    <description>Each client may perform its action twice when you lead or follow its role</description>
  </card>
  <card>
    <building>Coliseum</building>
    <material>stone</material>
    <description>When performing LEGIONARY action may take opponent's client and place in VAULT as material</description>
  </card>
  <card>
    <building>Garden</building>
    <material>stone</material>
    <description>When you complete a Garden, you may immediately execute one Patron action for each pint of Influence you have.</description>
  </card>
  <card>
    <building>Prison</building>
    <material>stone</material>
    <description>May exchange INFLUENCE for opponent's completed structure</description>
  </card>
  <card>
    <building>Scriptorium</building>
    <material>stone</material>
    <description>May use one MARBLE material to complete any structure</description>
  </card>
  <card>
    <building>Sewer</building>
    <material>stone</material>
    <description>May place Orders cards used to lead or follow into STOCKPILE at end of turn</description>
  </card>
  <card>
    <building>Villa</building>
    <material>stone</material>
    <description>When performing ARCHITECT action may complete Villa with one material</description>
  </card>
  <!--Wood-->
  <card>
    <building>Circus</building>
    <material>wood</material>
    <description>May play two cards of same role as JACK</description>
  </card>
  <card>
    <building>Dock</building>
    <material>wood</material>
    <description>Each time you execute a Laborer action you may choose to take an additional card from your hand and place it into your Stockpile as a Material.</description>
  </card>
  <card>
    <building>Market</building>
    <material>wood</material>
    <description>Maximum VAULT + 2</description>
  </card>
  <card>
    <building>Palisade</building>
    <material>wood</material>
    <description>Immune to LEGIONARY</description>
  </card>
</cards>";
    }
}