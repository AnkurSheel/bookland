import { css } from '@emotion/core';
import { graphql, useStaticQuery } from 'gatsby';
import BackgroundImage, { IFluidObject } from 'gatsby-background-image';
import React from 'react';
import { HeroImageQuery } from '../graphqlTypes';

const styles = {
    image: css({
        backgroundPosition: 'bottom 13% center',
        backgroundSize: 'cover',
        height: '30vh',
        '+ *': {
            marginTop: 0,
        },
    }),
};

const Hero = () => {
    const data: HeroImageQuery = useStaticQuery(graphql`
        query HeroImage {
            image: file(relativePath: { eq: "inaki-del-olmo-bookshelf.jpg" }) {
                sharp: childImageSharp {
                    fluid {
                        ...GatsbyImageSharpFluid_withWebp
                    }
                }
            }
        }
    `);
    const fluid = (data.image && data.image.sharp && (data.image.sharp.fluid as IFluidObject)) || undefined;
    return <BackgroundImage css={styles.image} Tag="section" fluid={fluid} fadeIn="soft"></BackgroundImage>;
};

export default Hero;
