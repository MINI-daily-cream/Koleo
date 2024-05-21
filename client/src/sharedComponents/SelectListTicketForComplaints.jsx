//import * as React from 'react';
import React, { useState, useEffect } from 'react';
import { FormControl, InputLabel, MenuItem, Select } from '@mui/material';
import apiBaseUrl from '../config';
import axios from 'axios';

const SelectTickets = ({onTicketChange, LabelName} ) => {
  const [ticket, setTicket] = useState('');
  const [ticketList, setTicketList] = useState([]);
  const [userId, setuserId] = useState(localStorage.getItem('id'))
  const [jwtToken, setJwtToken] = useState(localStorage.getItem('jwtToken'));
  //const dataArray = ['City 1', 'City 2', 'City 3'];

  const handleChange = (event) => {
    setTicket(event.target.value);
    onTicketChange(event.target.value);
  };
  function getTickets(){
    const fetchData = async () => {
      try {
        const response = await axios.get(`${apiBaseUrl}/api/Ticket/list-by-user/${userId}`, {
          headers: {
              'Content-Type': 'application/json',
              'Authorization': `Bearer ${jwtToken}`
          }
        });
        console.log(response.data);
        setTicketList(response.data);
      }
      catch(error) {
        if (error === 'Bad request') {
            console.error('user exists');
        } else {
            console.error('An error occurred:', error);
        }
      }
    }
    fetchData();
  }
  
  useEffect( () => {
    getTickets();
  }, [])
  return (
    <div>
      <FormControl sx={{ m: 1, minWidth: 400 }}>
        <InputLabel id="demo-simple-select-helper-label">{LabelName}</InputLabel>
        <Select
          labelId="demo-simple-select-helper-label"
          id="demo-simple-select-helper"
          value={ticket}
          label={LabelName}
          onChange={handleChange}
        >
        {ticketList.map((ticketValue, index) => (
            <MenuItem key={index} value={ticketValue.id}>
              {ticketValue.startStation} - {ticketValue.endStation}, {ticketValue.startDate}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </div>
  );
}
export default SelectTickets;