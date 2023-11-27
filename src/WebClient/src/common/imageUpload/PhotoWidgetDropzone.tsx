import { useCallback } from "react";
import {useDropzone} from 'react-dropzone';

interface Props {
    setFiles: (file: Blob | null, index: number) => void,
    index: number;
}

export default function PhotoWidgetDropzone({setFiles, index}: Props) {
    const dropzoneStyles = {
        borderRadius: '5px',
        textAlign: 'center' as 'center',
        height: '30px',
        width: '100%',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        cursor: 'pointer',
        color: '#8e8e8e',
        paddingBottom: "10px"
    };

    const dropzoneActive = {
        borderColor: "green"
    }

    const onDrop = useCallback((acceptedFiles: any) => {
        setFiles(acceptedFiles[0], index);
    }, [setFiles]);

    const {getRootProps, getInputProps, isDragActive} = useDropzone({onDrop});

    return(
        <div {...getRootProps()} style={isDragActive ? {...dropzoneStyles, ...dropzoneActive} : dropzoneStyles}>
            <input {...getInputProps()}/>
            <div>
                <p style={{color: "black", fontSize: "1rem", height: "fit-content"}}>Drop here or click to add image 📁</p>
            </div>
        </div>
    )
}
