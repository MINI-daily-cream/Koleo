//import * as React from 'react';
import React, { useState, useEffect } from 'react';
import { FormControl, InputLabel, MenuItem, Select } from '@mui/material';
import apiBaseUrl from '../config';

const SelectLabels = ({ onCityChange, LabelName} ) => {
  const [city, setCity] = useState('');
  const [cityList, setCityList] = useState([]);
  //const dataArray = ['City 1', 'City 2', 'City 3'];

  const handleChange = (event) => {
    setCity(event.target.value);
    onCityChange(event.target.value);
  };
  function getCities(){
    const xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE) {
            if (xhr.status === 200) {
                const response = JSON.parse(xhr.responseText);
                //const response = xhr.responseText;
                console.log(response);
                setCityList(response);
            } else {
                console.error('Błąd pobierania danych:', xhr.status);
                // Obsługa błędów
            }
        }
    };

    // xhr.open('GET', `https://localhost:5001/api/City/getCities`);
    xhr.open('GET', `${apiBaseUrl}/api/City/getCities`);
    xhr.send();
  }

  useEffect( () => {
    getCities();
  }, [])
  return (
    <div>
      <FormControl sx={{ m: 1, minWidth: 250 }}>
        <InputLabel id="demo-simple-select-helper-label">{LabelName}</InputLabel>
        <Select
          labelId="demo-simple-select-helper-label"
          id="demo-simple-select-helper"
          value={city}
          label={LabelName}
          onChange={handleChange}
        >
        {cityList.map((cityValue, index) => (
            <MenuItem key={index} value={cityValue.name}>
              {cityValue.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </div>
  );
}
export default SelectLabels;