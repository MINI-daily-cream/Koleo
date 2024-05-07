const connectionsData = [
    {
        id: "1",
        startStationId: 1,
        endStationId: 2,
        trainId: 123,
        startTime: Date.now,
        endTime: Date.now,
        kmNumber: 100,
        duration: { hours: 2, minutes: 30 }
    },
    {
        id: "2",
        startStationId: 1,
        endStationId: 2,
        trainId: 126,
        startTime: Date.now,
        endTime: Date.now,
        kmNumber: 10,
        duration: { hours: 2, minutes: 25 }
    },
    // Add more connection data as needed
];
const stationsData = [
    { id: 1, name: "Warszawa" },
    { id: 1, name: "Gdańsk" },
    // Add more station data as needed
];

const stationMap = stationsData.reduce((map, station) => {
    map[station.id] = station.name;
    return map;
}, {});

// Map through connectionsData and replace station IDs with station names
const connections = connectionsData.map(connection => {
    return {
        id: connection.id,
        startStation: stationMap[connection.startStationId],
        endStation: stationMap[connection.endStationId],
        // Other connection properties
    };
});