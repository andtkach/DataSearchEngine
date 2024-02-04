import { useEffect, useState } from "react";
import axios from "axios";

const LOADING_STATES = ["loading", "errored", "success"];

function useGeneralizedCrudMethods(urls, errorNotificationFn) {
  const [data, setData] = useState();
  const [total, setTotal] = useState(0);
  const [error, setError] = useState();
  const [loadingStatus, setLoadingStatus] = useState("loading");

  if (!urls) {
    throw "Error with urls";
  }

  function formatErrorString(e, url) {
    const errorString =
      e?.response?.status === 404
        ? e?.message + " url " + url
        : e?.message + e?.response?.data;
    console.error(errorString);
    return errorString;
  }

  useEffect(() => {
    async function getData() {
      try {
        setLoadingStatus(LOADING_STATES[0]);
        const results = await axios.get(urls.latest);
        setData(results.data);
        setLoadingStatus(LOADING_STATES[2]);
      } catch (e) {
        setError(e);
        setLoadingStatus(LOADING_STATES[1]);
      }
    }
    getData();
  }, [urls.latest]);

  useEffect(() => {
    async function getData() {
      try {
        setLoadingStatus(LOADING_STATES[0]);
        const results = await axios.get(urls.total);
        setTotal(results.data);
        setLoadingStatus(LOADING_STATES[2]);
      } catch (e) {
        setError(e);
        setLoadingStatus(LOADING_STATES[1]);
      }
    }
    getData();
  }, [urls.total]);

  function createRecord(createObject, callbackDone) {
    
    async function addData() {
      const startingData = data.map(function (rec) {
        return { ...rec };
      });
      try {
        setData(function (oriState) {
          return [createObject, ...oriState];
        });
        await axios.post(`${urls.mutate}`, createObject);
        if (callbackDone) callbackDone();
      } catch (e) {
        setData(startingData);
        const errorString = formatErrorString(e, urls.mutate);
        errorNotificationFn?.(errorString);
        if (callbackDone) callbackDone();
      }
    }
    addData();
  }

  function updateRecord(updateObject, callbackDone) {
    const id = updateObject.id; 
    async function updateData() {
      const startingData = data.map(function (rec) {
        return { ...rec };
      });
      try {
        setData(function (oriState) {
          const dataRecord = oriState.find((rec) => rec.id === id);

          // only update the fields passed in for the updateObject
          for (const [key, value] of Object.entries(updateObject)) {
            dataRecord[key] = value === undefined ? dataRecord[key] : value;
          }
          return oriState.map((rec) => (rec.id === id ? dataRecord : rec));
        });
        await new Promise((resolve) => setTimeout(resolve, 2000));

        const updatedRecord = data.find((rec) => rec.id === id);
        await axios.put(`${urls.mutate}/${id}`, updatedRecord);
        if (callbackDone) callbackDone();
      } catch (e) {
        setData(startingData);
        const errorString = formatErrorString(e, urls.mutate);
        errorNotificationFn?.(errorString);
        if (callbackDone) callbackDone();
      }
    }

    if (data.find((rec) => rec.id === id)) {
      updateData();
    } else {
      const errorString = `No data record found for id ${id}`;
      errorNotificationFn?.(errorString);
    }
  }
  function deleteRecord(id, callbackDone) {
    async function deleteData() {
      const startingData = data.map(function (rec) {
        return { ...rec };
      });
      try {
        setData(function (oriState) {
          return oriState.filter((rec) => rec.id != id);
        });
        await axios.delete(`${urls.mutate}/${id}`);
        if (callbackDone) callbackDone();
      } catch (e) {
        setData(startingData);
        const errorString = formatErrorString(e, urls.mutate);
        errorNotificationFn?.(errorString);
        if (callbackDone) callbackDone();
      }
    }
    if (data.find((rec) => rec.id === id)) {
      deleteData();
    } else {
      const errorString = `No data record found for id ${id}`;
      errorNotificationFn?.(errorString);
    }
  }

  function searchRecord(term) {
    async function getData(term) {
      try {
        setLoadingStatus(LOADING_STATES[0]);
        const results = await axios.get(`${urls.search}?term=${term}`);
        setData(results.data);
        setLoadingStatus(LOADING_STATES[2]);
      } catch (e) {
        setError(e);
        setLoadingStatus(LOADING_STATES[1]);
      }
    }
    getData(term);
  }

  return {    
    data,
    loadingStatus,
    error,
    total,
    createRecord,
    updateRecord,
    deleteRecord,
    searchRecord,
  };
}

export default useGeneralizedCrudMethods;
