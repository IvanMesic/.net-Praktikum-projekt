﻿using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MojZabavniDal.Model
{
    public class Match
    {
        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("fifa_id")]
        [Newtonsoft.Json.JsonConverter(typeof(ParseStringConverter))]
        public long FifaId { get; set; }

        [JsonProperty("weather")]
        public Weather Weather { get; set; }

        [JsonProperty("attendance")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Attendance { get; set; }

        [JsonProperty("officials")]
        public string[] Officials { get; set; }

        [JsonProperty("stage_name")]
        public string StageName { get; set; }

        [JsonProperty("home_team_country")]
        public string HomeTeamCountry { get; set; }

        [JsonProperty("away_team_country")]
        public string AwayTeamCountry { get; set; }

        [JsonProperty("datetime")]
        public DateTimeOffset? Datetime { get; set; }

        [JsonProperty("winner")]
        public string Winner { get; set; }

        [JsonProperty("winner_code")]
        public string WinnerCode { get; set; }

        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        [JsonProperty("away_team")]
        public Team AwayTeam { get; set; }

        [JsonProperty("home_team_events")]
        public TeamEvent[] HomeTeamEvents { get; set; }

        [JsonProperty("away_team_events")]
        public TeamEvent[] AwayTeamEvents { get; set; }

        [JsonProperty("home_team_statistics")]
        public TeamStatistics HomeTeamStatistics { get; set; }

        [JsonProperty("away_team_statistics")]
        public TeamStatistics AwayTeamStatistics { get; set; }

        [JsonProperty("last_event_update_at")]
        public DateTimeOffset? LastEventUpdateAt { get; set; }

        [JsonProperty("last_score_update_at")]
        public DateTimeOffset? LastScoreUpdateAt { get; set; }
    }

    public partial class Team
    {

        [JsonProperty("code")]
        private string Code { set { FifaCode = value; } }

        [JsonProperty("goals")]
        public long Goals { get; set; }

        [JsonProperty("penalties")]
        public long Penalties { get; set; }
    }

    public class TeamEvent
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type_of_event")]
        [JsonConverter(typeof(EventTypeConverter))]
        public EventType TypeOfEvent { get; set; }

        [JsonProperty("player")]
        public string Player { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
    }

    public class TeamStatistics
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("attempts_on_goal")]
        public long? AttemptsOnGoal { get; set; }

        [JsonProperty("on_target")]
        public long? OnTarget { get; set; }

        [JsonProperty("off_target")]
        public long? OffTarget { get; set; }

        [JsonProperty("blocked")]
        public long? Blocked { get; set; }

        [JsonProperty("woodwork")]
        public long? Woodwork { get; set; }

        [JsonProperty("corners")]
        public long? Corners { get; set; }

        [JsonProperty("offsides")]
        public long? Offsides { get; set; }

        [JsonProperty("ball_possession")]
        public long? BallPossession { get; set; }

        [JsonProperty("pass_accuracy")]
        public long? PassAccuracy { get; set; }

        [JsonProperty("num_passes")]
        public long? NumPasses { get; set; }

        [JsonProperty("passes_completed")]
        public long? PassesCompleted { get; set; }

        [JsonProperty("distance_covered")]
        public long? DistanceCovered { get; set; }

        [JsonProperty("balls_recovered")]
        public long? BallsRecovered { get; set; }

        [JsonProperty("tackles")]
        public long? Tackles { get; set; }

        [JsonProperty("clearances")]
        public long? Clearances { get; set; }

        [JsonProperty("yellow_cards")]
        public long? YellowCards { get; set; }

        [JsonProperty("red_cards")]
        public long? RedCards { get; set; }

        [JsonProperty("fouls_committed")]
        public long? FoulsCommitted { get; set; }

        [JsonProperty("tactics")]
        public string Tactics { get; set; }

        [JsonProperty("starting_eleven")]
        public List<Player> StartingEleven { get; set; }

        [JsonProperty("substitutes")]
        public List<Player> Substitutes { get; set; }
    }

    public partial class Weather
    {
        [JsonProperty("humidity")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Humidity { get; set; }

        [JsonProperty("temp_celsius")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TempCelsius { get; set; }

        [JsonProperty("temp_farenheit")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TempFarenheit { get; set; }

        [JsonProperty("wind_speed")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long WindSpeed { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public enum EventType { Goal, GoalPenalty, SubstitutionIn, SubstitutionOut, YellowCard, OwnGoal, RedCard, SecondYellowCard };

    public enum Position { Defender, Forward, Goalie, Midfield };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                EventTypeConverter.Singleton,
                PositionConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class EventTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EventType) || t == typeof(EventType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "goal":
                    return EventType.Goal;
                case "goal-penalty":
                    return EventType.GoalPenalty;
                case "substitution-in":
                    return EventType.SubstitutionIn;
                case "substitution-out":
                    return EventType.SubstitutionOut;
                case "yellow-card":
                    return EventType.YellowCard;
                case "yellow-card-second":
                    return EventType.SecondYellowCard;
                case "red-card":
                    return EventType.RedCard;
                case "goal-own":
                    return EventType.OwnGoal;
            }
            throw new Exception("Cannot unmarshal type EventType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (EventType)untypedValue;
            switch (value)
            {
                case EventType.Goal:
                    serializer.Serialize(writer, "goal");
                    return;
                case EventType.GoalPenalty:
                    serializer.Serialize(writer, "goal-penalty");
                    return;
                case EventType.SubstitutionIn:
                    serializer.Serialize(writer, "substitution-in");
                    return;
                case EventType.SubstitutionOut:
                    serializer.Serialize(writer, "substitution-out");
                    return;
                case EventType.YellowCard:
                    serializer.Serialize(writer, "yellow-card");
                    return;
            }
            throw new Exception("Cannot marshal type EventType");
        }

        public static readonly EventTypeConverter Singleton = new EventTypeConverter();
    }

    internal class PositionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Position) || t == typeof(Position?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Defender":
                    return Position.Defender;
                case "Forward":
                    return Position.Forward;
                case "Goalie":
                    return Position.Goalie;
                case "Midfield":
                    return Position.Midfield;
            }
            throw new Exception("Cannot unmarshal type Position");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Position)untypedValue;
            switch (value)
            {
                case Position.Defender:
                    serializer.Serialize(writer, "Defender");
                    return;
                case Position.Forward:
                    serializer.Serialize(writer, "Forward");
                    return;
                case Position.Goalie:
                    serializer.Serialize(writer, "Goalie");
                    return;
                case Position.Midfield:
                    serializer.Serialize(writer, "Midfield");
                    return;
            }
            throw new Exception("Cannot marshal type Position");
        }

        public static readonly PositionConverter Singleton = new PositionConverter();
    }
}
