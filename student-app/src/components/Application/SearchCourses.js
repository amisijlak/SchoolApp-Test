import React, { useState, useEffect } from 'react'
import { createAPIEndpoint, ENDPIONTS } from "../../api";
import { List, ListItem, ListItemText, Paper, InputBase, IconButton, makeStyles, ListItemSecondaryAction } from '@material-ui/core';
import SearchTwoToneIcon from '@material-ui/icons/SearchTwoTone';
import PlusOneIcon from '@material-ui/icons/PlusOne';
import ArrowForwardIosIcon from '@material-ui/icons/ArrowForwardIos';

const useStyles = makeStyles(theme => ({
    searchPaper: {
        padding: '2px 4px',
        display: 'flex',
        alignItems: 'center',
    },
    searchInput: {
        marginLeft: theme.spacing(1.5),
        flex: 1,
    },
    listRoot: {
        marginTop: theme.spacing(1),
        maxHeight: 450,
        overflow: 'auto',
        '& li:hover': {
            cursor: 'pointer',
            backgroundColor: '#E3E3E3'
        },
        '& li:hover .MuiButtonBase-root': {
            display: 'block',
            color: '#000',
        },
        '& .MuiButtonBase-root': {
            display: 'none'
        },
        '& .MuiButtonBase-root:hover': {
            backgroundColor: 'transparent'
        }
    }
}))

export default function SearchCourses(props) {

    const { values, setValues } = props;
    let appliedCourses = values.applicationDetails;

    const [courseUnits, setCourseUnits] = useState([]);
    const [searchList, setSearchList] = useState([]);
    const [searchKey, setSearchKey] = useState('');
    const classes = useStyles();

    useEffect(() => {
        createAPIEndpoint(ENDPIONTS.COURSE).fetchAll()
            .then(res => {
                setCourseUnits(res.data);
                setSearchList(res.data);
            })
            .catch(err => console.log(err))

    }, [])

    useEffect(() => {
        let x = [...courseUnits];
        x = x.filter(y => {
            return y.courseName.toLowerCase().includes(searchKey.toLocaleLowerCase())
                && appliedCourses.every(item => item.Id != y.courseUnitId)
        });
        setSearchList(x);
    }, [searchKey, appliedCourses])

    const addCourseUnit = courseunit => {
        let x = {
            ApplicationMasterId: values.applicationMasterId,
            applicationDetailId: 0,
            CourseDetailsId: courseunit.id,
            Frequency: 1,
            CoursePrice: courseunit.price,
            courseUnitName: courseunit.courseName
        }
        setValues({
            ...values,
            applicationDetails: [...values.applicationDetails, x]
        })
    }

    return (
        <>
            <Paper className={classes.searchPaper}>
                <InputBase
                    className={classes.searchInput}
                    value={searchKey}
                    onChange={e => setSearchKey(e.target.value)}
                    placeholder="Search course units" />
                <IconButton>
                    <SearchTwoToneIcon />
                </IconButton>
            </Paper>
            <List className={classes.listRoot}>
                {
                    searchList.map((item, idx) => (
                        <ListItem
                            key={idx}
                            onClick={e => addCourseUnit(item)}>
                            <ListItemText
                                primary={item.courseName}
                                secondary={'$' + item.price} />
                            <ListItemSecondaryAction>
                                <IconButton onClick={e => addCourseUnit(item)}>
                                    <PlusOneIcon />
                                    <ArrowForwardIosIcon />
                                </IconButton>
                            </ListItemSecondaryAction>
                        </ListItem>
                    ))
                }
            </List>
        </>
    )
}
