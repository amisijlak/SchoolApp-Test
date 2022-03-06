import React from 'react'
import { List, ListItemText, Paper, ListItem, ListItemSecondaryAction, IconButton, ButtonGroup, Button, makeStyles } from '@material-ui/core';
import DeleteTwoToneIcon from '@material-ui/icons/DeleteTwoTone';
import { roundTo2DecimalPoint } from "../../utils";

const useStyles = makeStyles(theme => ({
    paperRoot: {
        margin: '15px 0px',
        '&:hover': {
            cursor: 'pointer'
        },
        '&:hover $deleteButton': {
            display: 'block'
        }
    },
    buttonGroup: {
        backgroundColor: '#E3E3E3',
        borderRadius: 8,
        '& .MuiButtonBase-root ': {
            border: 'none',
            minWidth: '25px',
            padding: '1px'
        },
        '& button:nth-child(2)': {
            fontSize: '1.2em',
            color: '#000'
        }
    },
    deleteButton: {
        display: 'none',
        '& .MuiButtonBase-root': {
            color: '#E81719'
        },
    },
    totalPerItem: {
        fontWeight: 'bolder',
        fontSize: '1.2em',
        margin: '0px 10px'
    }
}))

export default function AppliedCourses(props) {

    const { values, setValues } = props;
    const classes = useStyles();

    let appliedCourses = values.applicationDetails;

    const removeCourseUnit = (index, id) => {
        debugger;
        let x = { ...values };
        x.applicationDetails = x.applicationDetails.filter((_, i) => i != index);
        if (id != 0)
            x.deletedCourseUnitIds += id + ',';
        setValues({ ...x });
    }

    const updateQuantity = (idx, value) => {
        let x = { ...values };
        let courseUnit = x.applicationDetails[idx];
        if (courseUnit.Frequency + value > 0) {
            courseUnit.Frequency += value;
            setValues({ ...x });
        }
    }

    return (
        <List>
            {appliedCourses.length == 0 ?
                <ListItem>
                    <ListItemText
                        primary="Please select course units"
                        primaryTypographyProps={{
                            style: {
                                textAlign: 'center',
                                fontStyle: 'italic'
                            }
                        }}
                    />
                </ListItem>
                : appliedCourses.map((item, idx) => (
                    <Paper key={idx} className={classes.paperRoot}>
                        <ListItem>
                            <ListItemText
                                primary={item.courseUnitName}
                                primaryTypographyProps={{
                                    component: 'h1',
                                    style: {
                                        fontWeight: '500',
                                        fontSize: '1.2em'
                                    }
                                }}
                                secondary={
                                    <>
                                        <ButtonGroup
                                            className={classes.buttonGroup}
                                            size="small">
                                            <Button onClick={e => updateQuantity(idx, -1)}>-</Button>
                                            <Button disabled >{item.Frequency}</Button>
                                            <Button onClick={e => updateQuantity(idx, 1)}>+</Button>
                                        </ButtonGroup>
                                        <span className={classes.totalPerItem}>
                                            {'$' + roundTo2DecimalPoint(item.Frequency * item.courseUnitPrice)}
                                        </span>
                                    </>
                                }
                                secondaryTypographyProps={{
                                    component: 'div'
                                }}
                            />
                            <ListItemSecondaryAction
                                className={classes.deleteButton}>
                                <IconButton
                                    disableRipple
                                    onClick={e => removeCourseUnit(idx, item.applicationDetailId)}
                                >
                                    <DeleteTwoToneIcon />
                                </IconButton>
                            </ListItemSecondaryAction>
                        </ListItem>
                    </Paper>
                ))
            }
        </List>
    )
}
