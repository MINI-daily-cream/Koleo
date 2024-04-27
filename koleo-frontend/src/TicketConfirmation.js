import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link } from "react-router-dom";
import React from 'react';
import { faUser, faMapMarkerAlt } from '@fortawesome/free-solid-svg-icons';

const TicketConfirmation = ({ticket }) => {
    
    const TravelTime = ({ date, timeDep, timeArr }) => {
        return (
            <div className="TravelTimeInfo">
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Odjazd</div>
                    <div className="TicketInfoColumnData">{timeDep}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Przyjazd</div>
                    <div className="TicketInfoColumnData">{timeArr}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Czas podróży</div>
                    <div className="TicketInfoColumnData">{timeDep}</div>
                </div>
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Data podróży</div>
                    <div className="TicketInfoColumnData">{date}</div>
                </div>
            </div>
        );
    };
    const TrainInfo = ({ trainNumber, wagonNumber, seatNumber }) => {
        return (
            <div className="TravelTimeInfo">
                <div className="TicketInfoColumn">
                    <div className="TicketInfoColumnText">Numer pociągu</div>
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
    const TravelerInfo = ({ name, surname }) => {
        return (
            <div className="TravelTravelerInfo">
                <h3>Dane podróżniczego</h3>
                <div className="ticket-details">
                    <div className="icon">
                        <FontAwesomeIcon icon={faUser} />
                    </div>
                    <div className="TicketInfoColumnText">{name} {surname}</div>
                </div>
            </div>
        );
    };
    
    const ticketData = {
        date: '2024-04-25',
        timeDep: '10:00',
        timeArr: '12:30',
        name: 'Jon',
        surname: 'Some',
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
            <div className="TravelDestInfo">
                  <div className="ticket-details">
                    <div className='text' id='od-do'>Od:</div>
                    <div className="icon">
                      <FontAwesomeIcon icon={faMapMarkerAlt} />
                    </div>
                    <div className='text'>{ticketData.departureStation}</div>
                    <div className='od-do-spacer' />
                    <div className='text' id='od-do'>Do:</div>
                    <div className="icon">
                      <FontAwesomeIcon icon={faMapMarkerAlt} />
                    </div>
                    <div className='text'>{ticketData.arrivalStation}</div>
                    </div>
            </div>
            <TravelTime date={ticketData.date} timeDep={ticketData.timeDep} timeArr={ticketData.timeArr} />
            <TrainInfo trainNumber={ticketData.trainNumber} wagonNumber={ticketData.wagonNumber} seatNumber={ticketData.seatNumber} />
            <TravelerInfo name={ticketData.name} surname={ticketData.surname} />
            <divc className="ButtonAligment">
            {/*TODO: set "to" prop*/}
                <Link to="/"><button type="submit" className="ConfirmationButton">Zmień dane</button></Link>
                <Link to="/"><button type="submit" className="ConfirmationButton">Kupuję</button></Link>
            </divc>
        </div>
    );
}
 
export default TicketConfirmation;