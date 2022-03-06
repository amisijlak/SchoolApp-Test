import React from 'react'
import ApplicationForm from './ApplicationForm'
import { useForm } from '../../hooks/useForm';
import { Grid } from '@material-ui/core';
import SearchCourses from './SearchCourses';
import AppliedCourses from './AppliedCourses';


const generateApplicationNumber = () => Math.floor(100000 + Math.random() * 900000).toString();

const getFreshModelObject = () => ({
    applicationMasterId: 0,
    ApplicationNumber: generateApplicationNumber(),
    studentId: 0,
    TeachingMethod: 'none',
    gTotal: 0,
    deletedMasterDetailsIds: '',
    applicationDetails: []
})


export default function Application() {

    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange,
        resetFormControls
    } = useForm(getFreshModelObject);

    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <ApplicationForm
                    {...{
                        values,
                        setValues,
                        errors,
                        setErrors,
                        handleInputChange,
                        resetFormControls
                    }}
                />
            </Grid>

            <Grid item xs={6}>
                <SearchCourses
                    {...{
                        values,
                        setValues
                    }}
                />
            </Grid>
            <Grid item xs={6}>
                <AppliedCourses
                    {...{
                        values,
                        setValues
                    }}
                />
            </Grid>
        </Grid>
    )
}
