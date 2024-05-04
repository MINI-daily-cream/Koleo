import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";
import React, { useState, useEffect } from 'react';
import { faUser, faMapMarkerAlt } from '@fortawesome/free-solid-svg-icons';
import TimeComponent from "./sharedComponents/TimeComponent.js";

const TicketConfirmation = ({ }) => { // here there is USERS id
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [connection, setConnection] = useState([]);
    const [mainConnection, setmainConnection] = useState(
        {startStation : '', endStation: '', startDate: '', endDate: '', startTime: '', endTime: ''}
    );
    const UserId = 1;

    useEffect(() => {
        // Fetch connection data when component mounts
        const fetchConnection = async () => {
            try {
                const response = await fetch("https://localhost:5001/api/Connection"); // Adjust the API endpoint URL
                if (response.ok) {
                    const data = await response.json();
                    setConnection(data);

                    const mappedData = {
                        startStation : data[0].startStation,
                        endStation : data[data.length-1].endStation,
                        startDate : data[0].startDate,
                        endDate : data[data.length-1].endDate,
                        startTime: data[0].startTime,
                        endTime: data[data.length-1].endTime,
                        sourceCity: data[0].sourceCity,
                        destinationCity: data[data.length-1].destinationCity,
                    }
                    // Set the mapped data to the state
                    setmainConnection(mappedData);
                } else {
                    console.error('Failed to fetch connection data');
                }
            } catch (error) {
                console.error('Error fetching connection data:', error);
            }
        };

        fetchConnection(); // Call the fetchConnection function
    }, []); // Empty dependency array t

    const handleNameChange = (e) => {
        setName(e.target.value);
    };
    const handleSurnameChange = (e) => {
        setSurname(e.target.value);
    };

    const handleBuyButtonClick = async () => {
        const requestBody = {
            connections: connection,
            targetName: name,
            targetSurname: surname
        };

        try {
            const response = await fetch(`/buy/${UserId}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(requestBody)
            });

            if (response.ok) {
                console.log('Ticket purchased successfully.');
            } else {
                console.error('Failed to purchase ticket.');
            }
        } catch (error) {
            console.error('Network error:', error);
        }
    }
    const ByStations = () => {
        return (
            <div className="TravelTimeInfo">
                <div className="separator">
                    <h3>Przez stacje:</h3>
                    <hr></hr>
                    {connection.map((con) => ( 
                        <div className="stationItem">
                            <FromStationToStation startStation={con.startStation} endStation={con.endStation}></FromStationToStation>
                            <TrainInfo trainNumber={con.providerName} wagonNumber={ticketData.wagonNumber} seatNumber={ticketData.seatNumber} />
                            <div className="TravelTimeInfo">
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Odjazd</div>
                                <div className="TicketInfoColumnData">
                                    <TimeComponent time={con.startTime}></TimeComponent>
                                </div>
                            </div>
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Przyjazd</div>
                                <div className="TicketInfoColumnData">
                                    <TimeComponent time={con.endTime}></TimeComponent>
                                </div>
                            </div>
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Czas podróży</div>
                                <div className="TicketInfoColumnData">{con.duration}</div>
                            </div>
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Data odjazdu</div>
                                <div className="TicketInfoColumnData">{con.startDate}</div>
                            </div>
                            <div className="TicketInfoColumn">
                                <div className="TicketInfoColumnText">Data przyjazdu</div>
                                <div className="TicketInfoColumnData">{con.endDate}</div>
                            </div>
                            </div>
                        </div>
                    ))}
                    <hr></hr>
                </div>
            </div>                
        );
    };
    const TravelTime = ({ startDate, endDate, timeDep, timeArr }) => {
        return (
            <div className="TravelTimeInfo">
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Odjazd</div>
                    <div className="TicketInfoColumnData">
                        <TimeComponent time={timeDep}></TimeComponent>
                    </div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Przyjazd</div>
                    <div className="TicketInfoColumnData">
                        <TimeComponent time={timeArr}></TimeComponent>
                    </div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Data odjazdu</div>
                    <div className="TicketInfoColumnData">{startDate}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Data przyjazdu</div>
                    <div className="TicketInfoColumnData">{endDate}</div>
                </div>
            </div>
        );
    };
    const TrainInfo = ({ trainNumber, wagonNumber, seatNumber }) => {
        return (
            <div className="TravelTimeInfo">
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Przewoźnik</div>
                    <div className="TicketInfoColumnData">{trainNumber}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Numer wagonu</div>
                    <div className="TicketInfoColumnData">{wagonNumber}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Numer miejsca</div>
                    <div className="TicketInfoColumnData">{seatNumber}</div>
                </div>
            </div>
        );
    };
    const FromStationToStation = ({startStation, endStation }) => {
        return (
            <div className="TravelDestInfo">
                    <div className="ticket-details">
                    <div className='text' id='od-do'>Od:</div>
                    <div className="icon">
                        <FontAwesomeIcon icon={faMapMarkerAlt} />
                    </div>     
                        <div className='text'>{startStation}</div>
                        <div className='od-do-spacer' />
                        <div className='text' id='od-do'>Do:</div>
                        <div className="icon">
                        <FontAwesomeIcon icon={faMapMarkerAlt} />
                        </div>
                        <div className='text'>{endStation}</div>
                    </div>
                </div>
        );
    };
    const ticketData = {
        date: '2024-04-25',
        timeDep: '10:00',
        timeArr: '12:30',
        //name: 'Jon',
        //surname: 'Some',
        trainNumber: '1234',
        finalStation: 'Destination',
        departureStation: 'Warszawa',
        arrivalStation: 'Gdańsk',
        wagonNumber: 'A12',
        seatNumber: '7',
    };
    return (
        <div>
            <div className="TicketInfoHeader">
                <p>Podsumowanie zakupu</p>
            </div>
            <form onSubmit={handleBuyButtonClick }>
                
                <FromStationToStation 
                    startStation={mainConnection.sourceCity} 
                    endStation={mainConnection.destinationCity}>
                </FromStationToStation>
                <TravelTime startDate={mainConnection.startDate} 
                    endDate={mainConnection.endDate} 
                    timeDep={mainConnection.startTime} 
                    timeArr={mainConnection.endTime} />
                <ByStations></ByStations>
                <div className="TravelTravelerInfo">
                    <h3>Dane podróżniczego</h3>
                    <div className="ticket-details">
                        <div className="icon">
                            <FontAwesomeIcon icon={faUser} />
                        </div>
                        <div className="TravelerInfoInput">
                            <label htmlFor="name">Imię:</label>
                            <input
                                type="text"
                                id="name"
                                value={name}
                                onChange={handleNameChange}
                                required
                            />
                        </div>
                        <div className="TravelerInfoInput">
                            <label htmlFor="surname">Nazwisko:</label>
                            <input
                                type="text"
                                id="surname"
                                value={surname}
                                onChange={handleSurnameChange}
                                required
                            />
                        </div>
                        {/* <input type="text" placeholder="Nazwisko" value={surname} onChange={handleSurnameChange}></input> */}
                    </div>
                </div>
                <div className="ButtonAligment">
                {/*TODO: set "to" prop*/}
                    <Link to="/"><button type="submit" className="ConfirmationButton">Wróć</button></Link>
                    <Link to="/"><button type="submit"
                        className="ConfirmationButton"
                        onClick={handleBuyButtonClick }
                    >Kupuję</button></Link>
                    </div>
            </form>
        </div>
    );
}
 
export default TicketConfirmation;