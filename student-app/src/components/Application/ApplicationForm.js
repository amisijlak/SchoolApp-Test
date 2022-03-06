import React, { useState, useEffect } from 'react'
import Form from "../../layouts/Form";
import { Grid, InputAdornment, makeStyles, ButtonGroup, Button as MuiButton } from '@material-ui/core';
import { Input, Select, Button } from "../../controls";
import ReplayIcon from '@material-ui/icons/Replay';
import SchoolIcon from '@material-ui/icons/School';
import ReorderIcon from '@material-ui/icons/Reorder';
import { createAPIEndpoint, ENDPIONTS } from "../../api";
import { roundTo2DecimalPoint } from "../../utils";
import Popup from '../../layouts/Popup';
import ApplicationList from './ApplicationList';
import Notification from "../../layouts/Notification";
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown'

const pMethods = [
    { id: 'none', title: 'Select' },
    { id: 'Online', title: 'Online' },
    { id: 'Physical', title: 'Physical' },
]

const useStyles = makeStyles(theme => ({
    adornmentText: {
        '& .MuiTypography-root': {
            color: '#f3b33d',
            fontWeight: 'bolder',
            fontSize: '1.5em'
        }
    },
    submitButtonGroup: {
        backgroundColor: '#f3b33d',
        color: '#000',
        margin: theme.spacing(1),
        '& .MuiButton-label': {
            textTransform: 'none'
        },
        '&:hover': {
            backgroundColor: '#f3b33d',
        }
    }
}))

export default function ApplicationForm(props) {

    const { values, setValues, errors, setErrors, handleInputChange, resetFormControls } = props;
    const classes = useStyles();

    const [studentList, setStudentList] = useState([]);
    const [applicationListVisibility, setApplicationListVisibility] = useState(false);
    const [applicationId, setApplicationId] = useState(0);
    const [notify, setNotify] = useState({ isOpen: false })

    useEffect(() => {
        createAPIEndpoint(ENDPIONTS.STUDENT).fetchAll()
            .then(res => {
                let studentList = res.data.map(item => ({
                    id: item.id,
                    title: item.studentName
                }));
                studentList = [{ id: 0, title: 'Select' }].concat(studentList);
                setStudentList(studentList);
            })
            .catch(err => console.log(err))
    }, [])


    useEffect(() => {
        let gTotal = values.applicationDetails.reduce((tempTotal, item) => {
            return tempTotal + (item.quantity * item.price);
        }, 0);
        setValues({
            ...values,
            gTotal: roundTo2DecimalPoint(gTotal)
        })

    }, [JSON.stringify(values.applicationDetails)]);

    useEffect(() => {
        if (applicationId == 0) resetFormControls()
        else {
            createAPIEndpoint(ENDPIONTS.APPLICATION).fetchById(applicationId)
                .then(res => {
                    setValues(res.data);
                    setErrors({});
                })
                .catch(err => console.log(err))
        }
    }, [applicationId]);

    const validateForm = () => {
        let temp = {};
        temp.studentId = values.studentId != 0 ? "" : "This field is required.";
        temp.TeachingMethod = values.TeachingMethod != "none" ? "" : "This field is required.";
        temp.applicationDetails = values.applicationDetails.length != 0 ? "" : "This field is required.";
        setErrors({ ...temp });
        return Object.values(temp).every(x => x === "");
    }

    const resetForm = () => {
        resetFormControls();
        setApplicationId(0);
    }

    const submitApplication = e => {
        e.preventDefault();
        if (validateForm()) {
            if (values.applicationMasterId == 0) {
                createAPIEndpoint(ENDPIONTS.APPLICATION).create(values)
                    .then(res => {
                        resetFormControls();
                        setNotify({ isOpen: true, message: 'New application is created.' });
                    })
                    .catch(err => console.log(err));
            }
            else {
                createAPIEndpoint(ENDPIONTS.APPLICATION).update(values.applicationMasterId, values)
                    .then(res => {
                        setApplicationId(0);
                        setNotify({ isOpen: true, message: 'The application is updated.' });
                    })
                    .catch(err => console.log(err));
            }
        }

    }

    const openListOfApplications = () => {
        setApplicationListVisibility(true);
    }

    return (
        <>
            <ButtonGroup className={classes.submitButtonGroup}>
                <MuiButton
                    size="large"
                    endIcon={<SchoolIcon />}
                    type="submit">Submit</MuiButton>
            </ButtonGroup>

            <Form onSubmit={submitApplication}>
                <Grid container>
                    <Grid item xs={6}>
                        <Input
                            disabled
                            label="Application Number"
                            name="ApplicationNumber"
                            value={values.ApplicationNumber}
                            InputProps={{
                                startAdornment: <InputAdornment
                                    className={classes.adornmentText}
                                    position="start">#</InputAdornment>
                            }}
                        />
                        <Select
                            label="Student"
                            name="studentId"
                            value={values.studentId}
                            onChange={handleInputChange}
                            options={studentList}
                            error={errors.studentId}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <Select
                            label="Teaching Method"
                            name="TeachingMethod"
                            value={values.TeachingMethod}
                            onChange={handleInputChange}
                            options={pMethods}
                            error={errors.TeachingMethod}
                        />
                        <Input
                            disabled
                            label="Grand Total"
                            name="gTotal"
                            value={values.gTotal}
                            InputProps={{
                                startAdornment: <InputAdornment
                                    className={classes.adornmentText}
                                    position="start">$</InputAdornment>
                            }}
                        />
                        <ButtonGroup className={classes.submitButtonGroup}>
                            <MuiButton
                                size="large"
                                endIcon={<SchoolIcon />}
                                type="submit">Submit</MuiButton>
                            <MuiButton
                                size="small"
                                onClick={resetForm}
                                startIcon={<ReplayIcon />}
                            />
                        </ButtonGroup>
                        <Button
                            size="large"
                            onClick={openListOfApplications}
                            startIcon={<ReorderIcon />}
                        >Applications </Button>
                    </Grid>
                </Grid>
            </Form>
            <Popup
                title="List of Applications"
                openPopup={applicationListVisibility}
                setOpenPopup={setApplicationListVisibility}>
                <ApplicationList
                    {...{ setApplicationId, setApplicationListVisibility, resetFormControls, setNotify }} />
            </Popup>
            <Notification
                {...{ notify, setNotify }} />
        </>
    )
}
