import React, { useState, useEffect } from 'react'
import { createAPIEndpoint, ENDPIONTS } from "../../api";
import Table from "../../layouts/Table";
import { TableHead, TableRow, TableCell, TableBody } from '@material-ui/core';
import DeleteOutlineTwoToneIcon from '@material-ui/icons/DeleteOutlineTwoTone';

export default function ApplicationList(props) {

    const { setApplicationId, setApplicationListVisibility, resetFormControls, setNotify } = props;

    const [applicationList, setApplicationList] = useState([]);

    useEffect(() => {
        createAPIEndpoint(ENDPIONTS.APPLICATION).fetchAll()
            .then(res => {
                setApplicationList(res.data)
            })
            .catch(err => console.log(err))
    }, [])

    const showForUpdate = id => {
        setApplicationId(id);
        setApplicationListVisibility(false);
    }

    const deleteApplication = id => {
        if (window.confirm('Are you sure to delete this application?')) {
            createAPIEndpoint(ENDPIONTS.APPLICATION).delete(id)
                .then(res => {
                    setApplicationListVisibility(false);
                    setApplicationId(0);
                    resetFormControls();
                    setNotify({ isOpen: true, message: 'Deleted successfully.' });
                })
                .catch(err => console.log(err))
        }
    }

    return (
        <>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Application No.</TableCell>
                        <TableCell>Student</TableCell>
                        <TableCell>Payed With</TableCell>
                        <TableCell>Grand Total</TableCell>
                        <TableCell></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        applicationList.map(item => (
                            <TableRow key={item.applicationMasterId}>
                                <TableCell
                                    onClick={e => showForUpdate(item.id)}>
                                    {item.applicationNumber}
                                </TableCell>
                                <TableCell
                                    onClick={e => showForUpdate(item.id)}>
                                    {item.student.studentName}
                                </TableCell>
                                <TableCell
                                    onClick={e => showForUpdate(item.id)}>
                                    {item.teachingMethod}
                                </TableCell>
                                <TableCell
                                    onClick={e => showForUpdate(item.id)}>
                                    {item.grandTotal}
                                </TableCell>
                                <TableCell>
                                    <DeleteOutlineTwoToneIcon
                                        color="secondary"
                                        onClick={e => deleteApplication(item.id)} />
                                </TableCell>

                            </TableRow>
                        ))
                    }
                </TableBody>
            </Table>
        </>
    )
}
